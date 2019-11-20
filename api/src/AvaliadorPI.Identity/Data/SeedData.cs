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

                //context.SaveChanges();
            }
        }
    }
}
