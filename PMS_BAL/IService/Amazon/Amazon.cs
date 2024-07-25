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
                await DeletedProduct();
                res.Data = amazonProducts;
                res.StatusCode = 200;
                res.Message = "Successfully SyncProducts";
                return res;
            }
            catch (Exception)
            {
                res.Data = new object();
                res.StatusCode = 500;
                res.Message = "Error SyncProducts";
                return res;
            }
        }

        public async Task DeletedProduct()
        {
            List<string> productskus =await _productRepository.GetProductsByUpdatedDate(DateTime.UtcNow); //GetData From DataBase
            List<AmazonProducts> amazonProducts = await _amazonRepository.GetProductsBySku(productskus);//GetData From Amazon
            if (amazonProducts.Count != productskus.Count)
            {
                // Find SKUs that are in `products` but not in `amazonProducts`
                List<string> deleteProductSkus = productskus.Except(amazonProducts.Select(p => p.sku)).ToList();
                await _productRepository.BulkDeleteProduct(deleteProductSkus);
            }
            const int batchSize = 100;
            int totalProducts = amazonProducts.Count;
            int batches = (int)Math.Ceiling((double)totalProducts / batchSize);

            for (int i = 0; i < batches; i++)
            {
                List<AmazonProducts> batchProducts = amazonProducts.Skip(i * batchSize).Take(batchSize).ToList();

                await ProcessBatch(batchProducts);
            }
        }

        private async Task ProcessBatch(List<AmazonProducts> batchProducts)
        {
            var productsToSave = new List<Products>();
            var productsToUpdate = new List<Products>();
            // Retrieve existing products in bulk based on SKUs
            var existingProducts = await _productRepository.GetBySku(batchProducts.Select(p => p.sku).ToList());

            foreach (var amazonProduct in batchProducts)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.sku == amazonProduct.sku);

                if (existingProduct != null)
                {
                    // Update existing product
                    existingProduct.Price = amazonProduct.Price;
                    existingProduct.UpdatedAt = DateTime.UtcNow;
                    existingProduct.IsActive = true;
                    existingProduct.IsDeleted = false;
                    productsToUpdate.Add(existingProduct);
                }
                else
                {
                    // Create new product
                    var newProduct = new Products
                    {
                        Name = amazonProduct.ProductName,
                        Price = amazonProduct.Price,
                        ProductType = "PMS_BAL.IService.Amazon.Amazon, PMS_BAL",
                        Quantity = amazonProduct.Quantity,
                        sku = amazonProduct.sku,
                        CreatedAt = DateTime.UtcNow,
                        //UpdatedAt = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false,
                    };
                    productsToSave.Add(newProduct);
                }
            }
            // Perform bulk operations
            if (productsToSave.Count>0)
            {
                await _productRepository.CreateBulk(productsToSave);
            }

            if (productsToUpdate.Count>0)
            {
                await _productRepository.BulkUpdateAsync(productsToUpdate);
            }
        }

        public async Task<JsonModel> GetProducts()
        {
            
            res.Data = await _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "Success";
            return res;
        }

        public async Task<JsonModel> CreateProductAsync(Products products)
        {
            products.ProductType = "PMS_BAL.IService.Amazon.Amazon, PMS_BAL";
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
