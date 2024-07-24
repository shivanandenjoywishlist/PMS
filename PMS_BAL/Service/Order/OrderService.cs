using COMMON;
using PMS_BAL.IService.Order;
using PMS_BAL.Service.Common;
using PMS_DAL.IRepositories.Order;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.Service.Order
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        JsonModel res = new JsonModel();

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<JsonModel> CreateOrderAsync(Orders order)
        {
            try
            {
                await _orderRepository.Create(order);
                res.Data = order;
                res.StatusCode = 200;
                res.Message = "OK";
                return res;
            }
            catch (Exception)
            {
                res.Data = order;
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
                return res;
            }

        }
        public async Task<JsonModel> GetOrders()
        {
            try
            {
                await _orderRepository.GetAll();
                res.Data = await _orderRepository.GetAll();
                res.StatusCode = 200;
                res.Message = "OK";
                return res;
            }
            catch (Exception)
            {
                res.Data = new object();
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
                return res;
            }
        }
    }
}