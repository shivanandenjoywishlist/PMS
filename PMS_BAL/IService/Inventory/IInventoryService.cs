using COMMON;
using PMS_BAL.IService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Inventory
{
    public interface IInventoryService<T> : IBaseService where T : class
    {
        Task<IEnumerable<T>> GetAll();
    }
}
