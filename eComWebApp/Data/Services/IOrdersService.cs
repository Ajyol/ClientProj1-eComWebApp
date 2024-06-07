using eComWebApp.Models;

namespace eComWebApp.Data.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task<Order> Update(int id);
        void Delete(int id);
    }
}
