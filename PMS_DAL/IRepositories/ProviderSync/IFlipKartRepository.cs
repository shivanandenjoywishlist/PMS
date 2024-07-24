using COMMON;
using PMS_DAL.IRepositories.Common;
using PMS_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_DAL.IRepositories.ProviderSync
{
    public interface IFlipKartRepository : IRepositoryBase<FlipKartProducts>
    {
        Task<List<FlipKartProducts>> GetProduct(string RefrenceId);
    }
}
