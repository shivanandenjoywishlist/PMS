using COMMON;
using PMS_BAL.IService.Product;
using PMS_BAL.Service.Common;
using PMS_DAL.IRepositories.Product;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMS_BAL.Service.Product
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
       // JsonModel res = new JsonModel();

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<JsonModel> GetProducts()
        {
            JsonModel res = new JsonModel();

            res.Data = await _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "Success";
            return res;
        }

        public async Task<Products> GetProductsById(int Id)
        {
            Products Data = await _productRepository.GetById(Id);
            return Data;
        }

        public async Task<JsonModel> CreateProductAsync(Products products)
        {
            JsonModel res = new JsonModel();

            try
            {
                await _productRepository.Create(products);
                res.Data = _productRepository.GetAll();
                res.StatusCode = 200;
                res.Message = "OK";
            }
            catch (Exception ex)
            {
                res.Data = new object();
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return res;
        }

        public async Task<JsonModel> UpdateProduct(Products products)
        {
            JsonModel res = new JsonModel();

            try
            {
                await _productRepository.Update(products);
                res.Data = products;
                res.StatusCode = 200;
                res.Message = "OK";
            }
            catch (Exception ex)
            {
                res.Data = new object();
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return res;
        }
        public async Task<JsonModel> DeleteProduct(int Id)
        {
            JsonModel res = new JsonModel();

            try
            {
                Products data = await _productRepository.GetById(Id);
                if (data != null)
                {
                    await _productRepository.Delete(data);
                    res.Data = data;
                    res.StatusCode = 200;
                    res.Message = "Successfully Product is Deleted";
                }
                else
                {
                    res.Data = null;
                    res.StatusCode = 404;
                    res.Message = "Not Found";
                }

            }
            catch (Exception ex)
            {
                res.Data = null;
                res.StatusCode = 500;
                res.Message = "Internal Server Error";
            }
            return res;
        }

    }
}
