using COMMON;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PMS_BAL.IService.Processor;
using PMS_BAL.IService.Product;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Amazon
{
    public class Amazon : IAmazon, IProcessor
    {
        private readonly IProductRepository _productRepository;
        private readonly IAmazonRepository _amazonRepository;
        JsonModel res = new JsonModel();
        public Amazon(IProductRepository productRepository, IAmazonRepository amazonRepository)
        {
            _productRepository = productRepository;
            _amazonRepository = amazonRepository;
        }
        public Task CreateOrder()
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<JsonModel> SyncProducts()
        {
            try
            {
                const int batchSize = 100;
                List<AmazonProducts> amazonProducts = await _amazonRepository.GetProduct("key");

                int totalProducts = amazonProducts.Count;
                int batches = (int)Math.Ceiling((double)totalProducts / batchSize);

                for (int i = 0; i < batches; i++)
                {
                    List<AmazonProducts> batchProducts = amazonProducts.Skip(i * batchSize).Take(batchSize).ToList();

                    await ProcessBatch(batchProducts);
                }
                res.Data = amazonProducts;
                res.StatusCode = 200;
                res.Message = "Successfully SyncProducts";
                return res;
            }
            catch (Exception ex)
            {
                res.Data = null;
                res.StatusCode = 500;
                res.Message = "Error SyncProducts";
                return res;
            }
        }

        private async Task ProcessBatch(List<AmazonProducts> batchProducts)
        {
            List<Products> productsToSave = new List<Products>();

            foreach (var amazonProduct in batchProducts)
            {
                var existingProduct = await _productRepository.GetBySkuAsync(amazonProduct.sku);

                if (existingProduct != null)
                {
                    existingProduct.Price = amazonProduct.Price;
                    existingProduct.UpdatedAt = DateTime.Now;
                    existingProduct.IsActive = true;
                    existingProduct.IsDeleted = false;

                    await _productRepository.UpdateAsync(existingProduct);
                }
                else
                {
                    await _productRepository.MarkAsDeletedBySkuAsync(amazonProduct.sku);

                    var newProduct = new Products
                    {
                        Name = amazonProduct.ProductName,
                        Price = amazonProduct.Price,
                        ProductType = "PMS_BAL.IService.Flipkart.Flipkart, PMS_BAL",
                        Quantity = amazonProduct.Quantity,
                        sku = amazonProduct.sku,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false,
                    };

                    productsToSave.Add(newProduct);
                }
            }

            if (productsToSave.Any())
            {
                await _productRepository.CreateBulk(productsToSave);
            }
        }



        public async Task<JsonModel> GetProducts()
        {
            JsonModel jsonModel = new JsonModel();
            jsonModel.Data = await _productRepository.GetAll();
            jsonModel.StatusCode = 200;
            jsonModel.Message = "Success";
            return jsonModel;
        }

        public async Task<JsonModel> CreateProductAsync(Products products)
        {
            products.ProductType = "PMS_BAL.IService.Flipkart.Flipkart, PMS_BAL";
            await _productRepository.Create(products);
            res.Data = _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }

        public async Task<JsonModel> UpdateProduct(Products products)
        {
            await _productRepository.Update(products);
            res.Data = products;
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }
        public async Task<JsonModel> DeleteProduct(int Id)
        {
            Products data = await _productRepository.GetById(Id);
            await _productRepository.Delete(data);
            res.Data = data;
            res.StatusCode = 200;
            res.Message = "OK";
            return res;
        }
    }
}
