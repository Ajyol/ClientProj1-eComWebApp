using eComWebApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eComWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Contact number is required")]
        public long PhoneNumber { get; set; }

        [Display(Name = "Service")]
        [Required(ErrorMessage = "Service order is required")]
        public List<OrderServices> Service { get; set; }


    }

}
