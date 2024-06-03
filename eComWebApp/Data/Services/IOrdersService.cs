using eComWebApp.Models;

namespace eComWebApp.Data.Services
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        Order Update(Order order);
        void Delete(Order order);
    }
}
