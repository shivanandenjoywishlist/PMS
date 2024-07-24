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

namespace PMS_BAL.Service.Product
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<JsonModel> GetProducts()
        {
            JsonModel jsonModel = new JsonModel();
            jsonModel.Data = await _productRepository.GetAll();
            jsonModel.StatusCode = 200;
            jsonModel.Message = "Success";
            return jsonModel;
        }

        public async Task<Products> GetProductsById(int Id)
        {
           
            Products Data = await _productRepository.GetById(Id);
           
            return Data;
        }

        public async Task<JsonModel> CreateProductAsync(Products products)
        {
            JsonModel res = new JsonModel();
            await _productRepository.Create(products);
            res.Data = _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }

        public async Task<JsonModel> UpdateProduct(Products products)
        {
            JsonModel res = new JsonModel();
            await _productRepository.Update(products);
            res.Data = products;
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }
        public async Task<JsonModel> DeleteProduct(int Id)
        {
            JsonModel res = new JsonModel();
            Products data = await _productRepository.GetById(Id);
            await _productRepository.Delete(data);
            res.Data = data;
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }

    }
}
