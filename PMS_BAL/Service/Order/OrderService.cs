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

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<JsonModel> CreateOrderAsync(Orders order)
        {
            JsonModel res = new JsonModel();
            _orderRepository.Create(order);
            res.Data = _orderRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }
        public async Task<JsonModel> GetOrders()
        {
            JsonModel res = new JsonModel();
            _orderRepository.GetAll();
            res.Data = _orderRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }
    }
}