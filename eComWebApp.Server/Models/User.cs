using eComWebApp.Data.Base;

namespace eComWebApp.Server.Models
{
    public class User : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
