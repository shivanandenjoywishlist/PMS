using COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_BAL.IService.Amazon;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Product;
using PMS_BAL.Service.Order;
using PMS_Entity;

namespace PMS.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsController : ControllerBase
    {

        public readonly IProductService _productService;
        public readonly IAmazon _iAmazon;

        public UserProductsController(IOrderService orderService, IAmazon iAmazon, IProductService productService)
        {
            _productService = productService;
            _iAmazon= iAmazon;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var res = _productService.ExcecuteFunction<Task<JsonModel>>(() => _productService.GetProducts());
            return Ok(res);
        }

        //[Authorize(Roles = "SuperUser")]
        //[HttpPost]
        //public ActionResult Create(Products products)
        //{
        //    var res = _productService.ExcecuteFunction<JsonModel>(() => _productService.CreateProductAsync(products));
        //    return Ok(res);
        //}

        [Authorize(Roles = "SuperUser")]
        [HttpPost]
        public ActionResult AmazonProductCreate(Products products)
        {
            var res = _productService.ExcecuteFunction<Task<JsonModel>>(() => _iAmazon.CreateProductAsync(products));
            return Ok(res);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpDelete]
        public ActionResult DeleteProduct(int Id)
        {
            var res = _productService.ExcecuteFunction<Task<JsonModel>>(() => _productService.DeleteProduct(Id));
            return Ok(res);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpPut]
        public ActionResult UpdateProduct(Products products)
        {
            var res = _productService.ExcecuteFunction<Task<JsonModel>>(() => _productService.UpdateProduct(products));
            return Ok(res);
        }

    }

}
