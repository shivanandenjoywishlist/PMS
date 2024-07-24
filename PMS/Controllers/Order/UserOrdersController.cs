using COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PMS.Controllers.Common;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Processor;
using PMS_BAL.Service.Order;
using PMS_Entity;



namespace PMS.Controllers.Order
{
    public class UserOrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IServiceProvider serviceProvider;

        public UserOrdersController(IOrderService orderService, IServiceProvider serviceProvider)
        {
            _orderService = orderService;
            this.serviceProvider = serviceProvider;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Json(await _orderService.ExcecuteFunction<Task<JsonModel>>(() => _orderService.GetOrders()));
        }
        
        [Authorize(Roles = "SuperUser")]
        [HttpPost]
        public async Task<ActionResult> Create(Orders orders)
        {
            return Json(await _orderService.ExcecuteFunction<Task<JsonModel>>(() => _orderService.CreateOrderAsync(orders)));
        }
        
    }
}
