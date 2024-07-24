using Azure;
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

namespace PMS_DAL.Repositories.ProviderSync
{
    public class FlipKartRepository : RepositoryBase<FlipKartProducts>, IFlipKartRepository
    {
        JsonModel responce = null;
        private readonly ApplicationContext _context;

        public FlipKartRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FlipKartProducts>> GetProduct(string RefrenceId)
        {
            List<FlipKartProducts> Data =  _context.FlipKartProducts.Where(x=>x.IsDeleted==false).ToList();
            return Data;
        }
    }
}
