using eComWebApp.Data;
using eComWebApp.Data.Enums;
using eComWebApp.Models;
using eComWebApp.Server.Models;
using Microsoft.AspNetCore.Identity;

public class AppDbInitializer
{
    public static async Task Initialize(ApplicationDbContext context, UserManager<User> userManager, ILogger logger)
    {
        try
        {
            logger.LogInformation("Starting database initialization...");

            if (!context.Users.Any())
            {
                await SeedUsers(userManager, logger);
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

    private static async Task SeedUsers(UserManager<User> userManager, ILogger logger)
    {
        var seededUser = new User
        {
            UserName = "admin",
            FirstName = "Ajyol",
            LastName = "Dhamala",
            Email = "ajyold81@gmail.com",
            DateOfBirth = new DateTime(2003, 7, 14)
        };

        var result = await userManager.CreateAsync(seededUser, "Admin@123");

        if (result.Succeeded)
        {
            logger.LogInformation("Seeded user 'admin' successfully.");
        }
        else
        {
            logger.LogError("Failed to seed user 'admin'. Errors: {0}", string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    private static async Task SeedOrders(ApplicationDbContext context)
    {
        var seededOrders = new List<Order>()
        {
            new Order
            {
                Name = "Washington DC",
                Address = "Name street 101",
                PhoneNumber = 9876546782,
                Email = "uscapital@gmail.com",
                Service = new List<OrderServices> { OrderServices.Service1, OrderServices.Service2 }
            }
        };

        context.Orders.AddRange(seededOrders);
        await context.SaveChangesAsync();
    }
}
