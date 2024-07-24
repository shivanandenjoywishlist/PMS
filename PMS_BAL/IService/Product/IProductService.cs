using COMMON;
using PMS_BAL.IService.Common;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Product
{
    public interface IProductService:IBaseService
    {
        Task<JsonModel> CreateProductAsync(Products product);
        Task<JsonModel> UpdateProduct(Products product);
        Task<JsonModel> GetProducts();
        Task<Products> GetProductsById(int Id);
        Task<JsonModel> DeleteProduct(int Id);
    }
}
