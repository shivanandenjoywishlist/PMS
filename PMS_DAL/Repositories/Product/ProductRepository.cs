using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.Common;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PMS_DAL.Repositories.Product
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetBySku(List<string> skus)
        {
            var products = await _context.Products
                .Where(p => skus.Contains(p.sku))
                .ToListAsync();
            return products;
        }

        public async Task BulkDeleteProduct(List<string> skus)
        {
            // Retrieve products based on SKUs
            List<Products> productsToDelete = await GetBySku(skus);

            // Update properties for deletion
            foreach (var product in productsToDelete)
            {
                product.IsDeleted = true;
                product.IsActive = false;
            }
            // Update range of products in the database
            _context.Products.UpdateRange(productsToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
           await _context.SaveChangesAsync();
        }
        public async Task BulkUpdateAsync(List<Products> product)
        {
            _context.Products.UpdateRange(product);
           await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetProductsByUpdatedDate(DateTime UpdatedDate)
        {
            UpdatedDate.AddHours(-5);
          return await _context.Products.Where(x => x.UpdatedAt < UpdatedDate).Select(p=>p.sku).ToListAsync();
        }
        public async Task MarkAsDeletedBySkuAsync(string sku)
        {
            var productsToDelete = await _context.Products
                .Where(p => p.sku == sku)
                .ToListAsync();

            foreach (var product in productsToDelete)
            {
                product.IsDeleted = true;
                product.IsActive = false;
                // Optionally, you can set UpdatedAt here if needed
            }

             await _context.SaveChangesAsync();
        }
    }
}
