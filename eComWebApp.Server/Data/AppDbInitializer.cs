using eComWebApp.Data;
using eComWebApp.Data.Enums;
using eComWebApp.Models;
using eComWebApp.Server.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppDbInitializer
{
    public static async Task Initialize(ApplicationDbContext context, ILogger logger)
    {
        try
        {
            logger.LogInformation("Starting database initialization...");

            if (!context.Users.Any())
            {
                await SeedUsers(context);
                logger.LogInformation("Users seeded successfully.");
            }
            else
            {
                logger.LogInformation("Users already seeded.");
            }

            if (!context.Orders.Any())
            {
                await SeedOrders(context);
                logger.LogInformation("Orders seeded successfully.");
            }
            else
            {
                logger.LogInformation("Orders already seeded.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    private static async Task SeedUsers(ApplicationDbContext context)
    {
        var seededUser = new User
        {
            UserName = "admin",
            FirstName = "Ajyol",
            LastName = "Dhamala",
            Email = "ajyold81@gmail.com",
            DateOfBirth = new DateTime(2003, 7, 14),
            PasswordHash = "Admin@123" 
        };

        context.Users.Add(seededUser);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(ApplicationDbContext context)
    {
        var seededOrders = new List<Order>()
        {
            new Order
            {
                Name = "Ajyol Dhamala",
                Address = "Name street 101",
                PhoneNumber = 9876546782,
                Email = "ajyold81@gmail.com",
                Service = new List<OrderServices> { OrderServices.Service1, OrderServices.Service2 }
            }
        };

        context.Orders.AddRange(seededOrders);
        await context.SaveChangesAsync();
    }
}
