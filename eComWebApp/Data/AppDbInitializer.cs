using eComWebApp.Data;
using eComWebApp.Data.Enums;
using eComWebApp.Models;

namespace eComWebApp.Data
{
    public class AppDbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                if (!context.Orders.Any())
                {
                    await SeedOrders(context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
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
            Email = "washington@gmail.com",
            Service = new List<OrderService> { OrderService.Service1, OrderService.Service2 }
        }
    };

            context.Orders.AddRange(seededOrders);
            await context.SaveChangesAsync();
        }

    }
}
