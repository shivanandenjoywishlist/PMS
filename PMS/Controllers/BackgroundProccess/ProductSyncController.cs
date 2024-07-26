using COMMON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Controllers.Common;
using PMS_BAL.IService.Amazon;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Processor;
using PMS_BAL.IService.Product;
using PMS_BAL.Service.Order;

namespace PMS.Controllers.BackgroundProccess
{
    public class ProductSyncController : BaseController
    {
        public readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;

        public ProductSyncController(IProductService productService, IServiceProvider serviceProvider)
        {
            _productService = productService;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> SyncProducts()
        {
            var product = await _productService.GetProductsById(12);
            var instance = (IProcessor)_serviceProvider.GetService(Type.GetType(product.ProductType));
            return Json(await _productService.ExcecuteFunction<Task<JsonModel>>(() => instance.SyncProducts()));
        }
    }
}
