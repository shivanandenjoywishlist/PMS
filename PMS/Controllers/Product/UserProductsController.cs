using COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Controllers.Common;
using PMS_BAL.IService.Amazon;
using PMS_BAL.IService.Order;
using PMS_BAL.IService.Product;
using PMS_BAL.Service.Order;
using PMS_Entity;

namespace PMS.Controllers.Product
{
    public class UserProductsController : BaseController
    {

        public readonly IProductService _productService;

        public UserProductsController(IOrderService orderService, IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _productService.ExcecuteFunction<Task<JsonModel>>(() => _productService.GetProducts()));
        }

        [Authorize(Roles = "SuperUser")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            return Json(await _productService.ExcecuteFunction<Task<JsonModel>>(() =>  _productService.DeleteProduct(Id)));
        }

        [Authorize(Roles = "SuperUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Products products)
        {
            return Json(await _productService.ExcecuteFunction<Task<JsonModel>>(() => _productService.UpdateProduct(products)));
        }
    }

}
