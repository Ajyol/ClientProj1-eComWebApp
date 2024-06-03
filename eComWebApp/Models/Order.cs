using eComWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eComWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        public long PhoneNumber { get; set; }

        [Display(Name = "Service")]
        public List<OrderService> Service { get; set; }


    }

    public class OrdersGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public OrderService Service { get; set; }
    }
}
