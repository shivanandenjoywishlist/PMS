using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Inventory
{
    public interface IInventoryServiceFactory
    {
        IInventoryService<T> GetInventoryService<T>() where T : class;
    }
}
