using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AvaliadorPI.Data.Context
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AvaliadorPIContext(serviceProvider.GetRequiredService<DbContextOptions<AvaliadorPIContext>>()))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                context.SaveChanges();
            }
        }
    }
}
