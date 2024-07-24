using COMMON;
using PMS_BAL.IService.Common;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Order
{
    public interface IOrderService : IBaseService
    {
        Task<JsonModel> CreateOrderAsync(Orders order);
        Task<JsonModel> GetOrders();
    }
}
