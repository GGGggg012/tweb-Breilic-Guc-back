using System;
using System.Linq;
using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities;

namespace eUseControl.DataAccess
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            SeedUsers(context);
            SeedProducts(context);
        }

        private static void SeedUsers(AppDbContext context)
        {
            if (context.Users.Any()) return;

            var admin = new UserData
            {
                FirstName = "Admin",
                Username = "admin",
                Email = "admin@eaviasales.com",
                Phone = "0000000000",
                Role = "Admin",
                RegisteredOn = DateTime.UtcNow
            };
            // forgot to call SetPasswordHash here - will fix in next commit
            admin.SetPasswordHash("admin123");

            context.Users.Add(admin);
            context.SaveChanges();
        }

        private static void SeedProducts(AppDbContext context)
        {
            if (context.Products.Any()) return;

            context.Products.AddRange(
                new Product { Name = "Economy Ticket", Description = "Basic seat", Price = 120.00m, Stock = 100 },
                new Product { Name = "Business Ticket", Description = "Business class", Price = 450.00m, Stock = 30 },
                new Product { Name = "First Class Ticket", Description = "Premium seat", Price = 980.00m, Stock = 10 }
            );
            context.SaveChanges();
        }
    }
}
