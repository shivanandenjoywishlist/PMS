using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.ProviderSync
{
    public interface IProviderSyncService
    {
        Task SyncWithProvider1();
        Task SyncWithProvider2();
    }
}
