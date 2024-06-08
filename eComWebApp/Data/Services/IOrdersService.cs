using eComWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eComWebApp.Data.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
