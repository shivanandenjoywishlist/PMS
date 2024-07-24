using COMMON;
using PMS_BAL.IService.Processor;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Flipkart
{
    public class Flipkart : IFlipkart, IProcessor
    {
        private IProductRepository _productRepository;
        private IFlipKartRepository _flipKartRepository;

        public Flipkart(IProductRepository productRepository, IFlipKartRepository flipKartRepository)
        {
            _productRepository = productRepository;
            _flipKartRepository=flipKartRepository;
        }
        public Task CreateOrder()
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetDetails()
        {
            throw new NotImplementedException();
        }

        public async Task SyncProducts()
        {
            const int batchSize = 100;
            List<FlipKartProducts> flipKartProducts = await _flipKartRepository.GetProduct("key"); // Assuming this retrieves all products

            int totalProducts = flipKartProducts.Count;
            int batches = (int)Math.Ceiling((double)totalProducts / batchSize);

            for (int i = 0; i < batches; i++)
            {
                List<FlipKartProducts> batchProducts = flipKartProducts.Skip(i * batchSize).Take(batchSize).ToList();

                List<Products> productsToSave = batchProducts.Select(p => new Products
                {
                    Name = p.ProductName,
                    Price = p.Price,
                    ProductType = "PMS_BAL.IService.Flipkart.Flipkart, PMS_BAL",
                    Quantity = p.Quantity,
                }).ToList();

                if (productsToSave.Any())
                {
                    await _productRepository.CreateBulk(productsToSave);
                }
            }
        }

        public async Task<JsonModel> GetProducts()
        {
            JsonModel jsonModel = new JsonModel();
            jsonModel.Data = _productRepository.GetAll();
            jsonModel.StatusCode = 200;
            jsonModel.Message = "Success";
            return jsonModel;
        }

        public async Task<JsonModel> CreateProductAsync(Products products)
        {
            JsonModel res = new JsonModel();
            products.ProductType = "PMS_BAL.IService.Flipkart.Flipkart, PMS_BAL";
            _productRepository.Create(products);
            res.Data = _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }

        public async Task<JsonModel> UpdateProduct(Products products)
        {
            JsonModel res = new JsonModel();
            _productRepository.Update(products);
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
