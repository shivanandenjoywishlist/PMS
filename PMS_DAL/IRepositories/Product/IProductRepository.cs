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
        Task<Products> GetBySkuAsync(string sku);
        Task UpdateAsync(Products product);
        Task MarkAsDeletedBySkuAsync(string sku);
    }
}
