using PMS_BAL.IService.ProviderSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.Service.ProviderSync
{
    public class ProviderSyncService : IProviderSyncService
    {
        public async Task SyncWithProvider1()
        {
            // Logic to sync with Provider1
            await Task.Delay(100); // Placeholder for actual synchronization logic
        }

        public async Task SyncWithProvider2()
        {
            // Logic to sync with Provider2
            await Task.Delay(100); // Placeholder for actual synchronization logic
        }
    }
}