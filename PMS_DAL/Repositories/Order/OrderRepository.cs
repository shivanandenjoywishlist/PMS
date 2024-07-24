using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.Common;
using PMS_DAL.IRepositories.Order;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using PMS_Entity;


namespace PMS_DAL.Repositories.Order
{
    public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
    {
        private readonly ApplicationContext _context;
        
        public OrderRepository(ApplicationContext context):base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task CreateOrderAsync(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Orders order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}