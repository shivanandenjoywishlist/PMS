using COMMON;
using Microsoft.EntityFrameworkCore;
using PMS_DAL.IRepositories.ProviderSync;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMS_DAL.Repositories.ProviderSync
{
    public class AmazonRepository: IAmazonRepository
    {
        private readonly ApplicationContext _context;
        public AmazonRepository(ApplicationContext context)
        {
            _context=context;
        }

        public async Task<List<AmazonProducts>> GetProduct(string RefrenceId)
        {
            List<AmazonProducts> data = new List<AmazonProducts>();
            try
            {
                data =  _context.AmazonProducts.Where(x=>x.IsDeleted==false)
                                     // .Where(p => p.RefrenceId == RefrenceId)
                                      .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return data;
        }

        public async Task<List<AmazonProducts>> GetProductsBySku(List<string> skus)
        {
            return await _context.AmazonProducts
                .Where(p => skus.Contains(p.sku))
                .ToListAsync();
        }

    }
}
