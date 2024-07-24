using COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Processor;
using PMS_BAL.Service.Order;
using PMS_Entity;



namespace PMS.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserOrdersController : ControllerBase
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
            var res = _orderService.ExcecuteFunction<Task<JsonModel>>(() => _orderService.GetOrders());
            return Ok(res);
        }
        
        [Authorize(Roles = "SuperUser")]
        [HttpPost]
        public async Task<ActionResult> Create(Orders orders)
        {
            //var product = _producContext.
            //string serviceType = "Amazone";
            //var instance = this.serviceProvider.GetService(Type serviceType);

            //instance.CreateOrder();

           var res =_orderService.ExcecuteFunction<Task<JsonModel>>(()=> _orderService.CreateOrderAsync(orders));
            return Ok(res);
        }
        
        //internal class Product
        //{
        //    string ProviderType { get; set; }
        //}
        
    }
}
