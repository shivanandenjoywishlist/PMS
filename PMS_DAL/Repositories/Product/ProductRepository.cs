using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.Common;
using PMS_DAL.IRepositories.Product;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Products> GetBySkuAsync(string sku)
        {
            return  _context.Products.FirstOrDefault(p => p.sku == sku);
            //return await _context.Products.FirstOrDefaultAsync(p => p.sku == sku);
        }

        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
             _context.SaveChanges();
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

             _context.SaveChanges();
        }
    }
}
