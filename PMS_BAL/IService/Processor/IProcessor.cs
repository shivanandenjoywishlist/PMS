using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Processor
{
    public interface IProcessor
    {
        Task<dynamic> GetDetails();
        Task CreateOrder();
        Task SyncProducts();
    }
}
