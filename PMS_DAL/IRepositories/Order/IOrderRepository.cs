using COMMON;
using PMS_DAL.IRepositories.Common;
using PMS_Entity; // Import the correct namespace for Order entity

namespace PMS_DAL.IRepositories.Order
{
    public interface IOrderRepository: IRepositoryBase<Orders>
    {
        Task<IEnumerable<Orders>> GetOrdersAsync();
        Task<Orders> GetOrderByIdAsync(int id);
        Task CreateOrderAsync(Orders order);
        Task UpdateOrderAsync(Orders order);
        Task DeleteOrderAsync(int id);
    }
}
