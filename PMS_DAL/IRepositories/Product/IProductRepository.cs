using PMS_DAL.IRepositories.Common;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_DAL.IRepositories.Product
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        Task<List<Products>> GetBySku(List<string> skus);
        Task BulkDeleteProduct(List<string> skus);
        Task UpdateAsync(Products product);
        Task MarkAsDeletedBySkuAsync(string sku);
        Task<List<string>> GetProductsByUpdatedDate(DateTime UpdatedDate);
        Task BulkUpdateAsync(List<Products> product);
    }
}
