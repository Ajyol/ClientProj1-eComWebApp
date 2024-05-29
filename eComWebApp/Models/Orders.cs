using eComWebApp.Data.Enums;

namespace eComWebApp.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public List<OrderService> Service { get; set; }


    }
}
