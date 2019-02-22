using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AvaliadorPI.Identity.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Roles.Any())
                {
                    string[] roles = new string[] { "Administrador", "Professor", "Aluno", "Avaliador" };

                    foreach (string role in roles)
                    {
                        var roleStore = new RoleStore<IdentityRole>(context);
                        roleStore.CreateAsync(new IdentityRole(role));
                    }
                }

                var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context),
                    null, null, null, null, null, null, null, null);

                if (!context.Users.Any(x => x.Email == "admin@ucl.br"))
                {
                    var user = new IdentityUser
                    {
                        Email = "admin@ucl.br",
                        UserName = "admin@ucl.br",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user, "tcc");
                    user.PasswordHash = hashed;

                    userManager.CreateAsync(user);
                    userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Administrador"));
                }

                if (!context.Users.Any(x => x.Email == "andrers@ucl.br"))
                {
                    var user = new IdentityUser
                    {
                        Id = "822f4e45-80fd-44be-a699-91f5ec9f908d",
                        Email = "andrers@ucl.br",
                        UserName = "andrers@ucl.br",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    var password = new PasswordHasher<IdentityUser>();
                    var hashed = password.HashPassword(user, "tcc");
                    user.PasswordHash = hashed;

                    var x = userManager.CreateAsync(user).Result;

                    userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Administrador"));
                    userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Avaliador"));
                    userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Professor"));
                }

                CadastrarAvaliador("d9307f03-aa97-4a82-aa57-08d5d8d85eef", "jefferson@ucl.br", userManager, context);
                CadastrarAvaliador("a62613e5-2eb7-4d54-aa58-08d5d8d85eef", "victorrosetti@ucl.br", userManager, context);
                CadastrarAvaliador("6d28edba-4ffb-491e-aa59-08d5d8d85eef", "jp@ucl.br", userManager, context);
                CadastrarAvaliador("778468db-c669-4a07-aa5a-08d5d8d85eef", "marlonferrari@ucl.br", userManager, context);
                CadastrarAvaliador("48596678-2044-4253-aa5b-08d5d8d85eef", "cledsonsm@ucl.br", userManager, context);
                CadastrarAvaliador("0a8dc569-0c93-4a10-aa5c-08d5d8d85eef", "jadsonrafalski@ucl.br", userManager, context);
                CadastrarAvaliador("02865888-2f78-4d84-8280-7f6b303aaff0", "zirlene@ucl.br", userManager, context);

                context.SaveChanges();
            }
        }

        private static void CadastrarAvaliador(string id, string email, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            if (!context.Users.Any(x => x.Email == email))
            {
                var user = new IdentityUser
                {
                    Id = id,
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "uclpi");
                user.PasswordHash = hashed;

                var x = userManager.CreateAsync(user).Result;
                var y = userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "Avaliador")).Result;
            }
        }
    }
}
