using AvaliadorPI.Identity.ViewModels;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AvaliadorPI.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        [TempData]
        public string StatusMessage { get; set; }

        public AccountController(IIdentityServerInteractionService interaction, IEventService events,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interaction = interaction;
            _events = events;
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Username);

                if (user == null)
                {
                    ModelState.AddModelError("", "Usuário não encontrado!");
                    return View(model);
                }

                if (model.TrocarSenha)
                {
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var senha = new Random(DateTime.Now.Millisecond).Next(10000, 99999).ToString();
                    var result = await _userManager.ResetPasswordAsync(user, token, senha);

                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(model);
                    }


                    try
                    {
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("avaliadorpi@gmail.com", "Senha@123")
                        };

                        using (var message = new MailMessage()
                        {
                            From = new MailAddress(model.Username, "Avaliador PI"),
                            Subject = "Acesso Concedido",
                            Body = $@"
Prezado, 

Sua senha de acesso ao aplicativo Avaliador PI foi alterada.

Suas credenciais são:

Username: {model.Username}
Password: {senha}

Att,
Avaliador PI
 
",
                            IsBodyHtml = false
                        })
                        {
                            message.To.Add(model.Username);
                            smtp.Send(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        return View(new NewUserViewModel { StatusMessage = "Usuario Criado, mas não foi possível enviar a senha: " + senha + " (" + ex.Message + ")" });
                    }
                }

                if (model.Adminsitrador)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (!claims.Any(x => x.Value == "Administrador"))
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Administrador"));
                    }
                }

                if (model.Avaliador)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (!claims.Any(x => x.Value == "Avaliador"))
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Avaliador"));
                    }
                }

                return View(new EditUserViewModel { StatusMessage = "Usuario Editado!" });
            }
            catch (Exception ex)
            {
                return View(new NewUserViewModel { StatusMessage = "Erro ao editar: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

                    AuthenticationProperties props = null;
                    if (model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1))
                        };
                    };

                    await HttpContext.SignInAsync(user.Id, user.UserName, props);

                    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("~/");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                ModelState.AddModelError("", "Credenciais Inválidas");
            }

            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                return await Logout(vm);
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();
                await _signInManager.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetDisplayName()));
            }

            return View("LoggedOut", vm);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar o usuario '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage, ReturnUrl = returnUrl, };

            return View(model);
        }

        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(NewUserViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);

            if (user != null)
            {
                ModelState.AddModelError("", "Usuário já cadastrado!");
                return View(model);
            }

            var senha = new Random(DateTime.Now.Millisecond).Next(10000, 99999).ToString();

            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Id = model.UsuarioId,
                Email = model.Username,
                UserName = model.Username,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            }, senha);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            user = await _userManager.FindByIdAsync(model.UsuarioId);

            if (model.Adminsitrador)
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Administrador"));
            }

            if (model.Avaliador)
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Avaliador"));
            }
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("avaliadorpi@gmail.com", "Senha@123")
                };

                using (var message = new MailMessage()
                {
                    From = new MailAddress(model.Username, "Avaliador PI"),
                    Subject = "Acesso Concedido",
                    Body = $@"
Prezado, 

Seu usuário foi liberado para acessar o aplicativo Avaliador PI.

Suas credenciais de acesso são:

Username: {model.Username}
Password: {senha}

Att,
Avaliador PI
 
",
                    IsBodyHtml = false
                })
                {
                    message.To.Add(model.Username);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                return View(new NewUserViewModel { StatusMessage = "Usuario Criado, mas não foi possível enviar a senha: " + senha + " (" + ex.Message + ")" });
            }

            return View(new NewUserViewModel { StatusMessage = "Usuario Criado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            //_logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = true };

            if (User?.Identity.IsAuthenticated != true)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = true,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            return vm;
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context?.IdP != null)
            {
                return new LoginViewModel
                {
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };
            }

            return new LoginViewModel
            {
                AllowRememberLogin = true,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}