using eComWebApp.Data;
using eComWebApp.Models;

namespace eComWebApp.Data
{
    public class AppDbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                SeedOrders(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.")
            };
        }
        
        private static async Task SeedOrders(ApplicationDbContext context)
        {
            if (context.Orders.Any())
            {
                var seededOrders = new List<Orders>()
                {
                    new()
                    {
                        Name = "Washington DC",
                        Address = "Name street 101",
                        PhoneNumber = 9876546782,
                        Email = "washingtion@gmail.com"
                    }
                };

                context.Orders.AddRange(seededOrders);
                await context.SaveChangesAsync();
            }
        }
    }
}
