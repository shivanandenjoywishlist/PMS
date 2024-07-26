using COMMON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Controllers.Common;
using PMS_BAL.IService.Inventory;
using PMS_Entity;

namespace PMS.Controllers.Inventory
{
   
    public class InventoryController : BaseController
    {
        private readonly IInventoryServiceFactory _inventoryServiceFactory;

        public InventoryController(IInventoryServiceFactory inventoryServiceFactory)
        {
            _inventoryServiceFactory = inventoryServiceFactory ?? throw new ArgumentNullException(nameof(inventoryServiceFactory));
        }

        [HttpGet]
        public async Task<IActionResult> ProductAvailability()
        {
            try
            {
                var inventoryService = _inventoryServiceFactory.GetInventoryService<ProductInventory>();
                var products = await inventoryService.GetAll();
                return Ok(products);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving product availability");
            }
        }
    }
}
