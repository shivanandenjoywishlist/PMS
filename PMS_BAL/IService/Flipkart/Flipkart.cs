using COMMON;
using PMS_BAL.IService.Processor;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_DAL.Repositories.ProviderSync;
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
        JsonModel res = new JsonModel();

        public Flipkart(IProductRepository productRepository, IFlipKartRepository flipKartRepository)
        {
            _productRepository = productRepository;
            _flipKartRepository = flipKartRepository;
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
                List<FlipKartProducts> flipkartProducts = await _flipKartRepository.GetProduct("key");
                int totalProducts = flipkartProducts.Count;
                int batches = (int)Math.Ceiling((double)totalProducts / batchSize);

                for (int i = 0; i < batches; i++)
                {
                    List<FlipKartProducts> batchProducts = flipkartProducts.Skip(i * batchSize).Take(batchSize).ToList();

                    await ProcessBatch(batchProducts);
                }
                res.Data = flipkartProducts;
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
        private async Task ProcessBatch(List<FlipKartProducts> batchProducts)
        {
            List<Products> productsToSave = new List<Products>();

            foreach (var flipkartProduct in batchProducts)
            {
                var existingProduct = await _productRepository.GetBySkuAsync(flipkartProduct.sku);

                if (existingProduct != null)
                {
                    existingProduct.Price = flipkartProduct.Price;
                    existingProduct.UpdatedAt = DateTime.Now;
                    existingProduct.IsActive = true;
                    existingProduct.IsDeleted = false;

                    await _productRepository.UpdateAsync(existingProduct);
                }
                else
                {
                    await _productRepository.MarkAsDeletedBySkuAsync(flipkartProduct.sku);

                    var newProduct = new Products
                    {
                        Name = flipkartProduct.ProductName,
                        Price = flipkartProduct.Price,
                        ProductType = "PMS_BAL.IService.Flipkart.Flipkart, PMS_BAL",
                        Quantity = flipkartProduct.Quantity,
                        sku = flipkartProduct.sku,
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
            res.Data = await _productRepository.GetAll();
            res.StatusCode = 200;
            res.Message = "Success";
            return res;
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
