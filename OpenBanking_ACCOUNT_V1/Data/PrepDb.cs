using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext? context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

           

            if (!context.Accounts.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Accounts.AddRange(
                    new Account() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Account() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Account() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
