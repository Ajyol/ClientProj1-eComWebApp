using eComWebApp.Data;
using eComWebApp.Data.Base;
using eComWebApp.Server.Models;

namespace eComWebApp.Server.Data.Services
{
    public class UsersService : EntityBaseRepository<User>, IUsersService
    {
        public UsersService(ApplicationDbContext context) : base(context) { }

    }
}
