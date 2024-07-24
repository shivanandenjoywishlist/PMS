using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_BAL.IService.Amazon;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Processor;
using PMS_BAL.IService.Product;

namespace PMS.Controllers.BackgroundProccess
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSyncController : ControllerBase
    {
        public readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;

        public ProductSyncController(IProductService productService, IServiceProvider serviceProvider)
        {
            _productService = productService;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _productService.GetProductsById(1);
            
            var instance = (IProcessor)_serviceProvider.GetService(Type.GetType(product.ProductType));

            instance.SyncProducts();

            return Ok();
        }
    }
}
