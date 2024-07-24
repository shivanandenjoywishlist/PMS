using PMS_BAL.IService.Common;
using PMS_BAL.IService.Inventory;
using PMS_DAL.IRepositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.Service.Inventory
{
    public class InventoryServiceFactory : IInventoryServiceFactory
    {
        private readonly IRepositoryBaseFactory _repositoryBaseFactory;

        public InventoryServiceFactory(IRepositoryBaseFactory repositoryBaseFactory)
        {
            _repositoryBaseFactory = repositoryBaseFactory ?? throw new ArgumentNullException(nameof(repositoryBaseFactory));
        }

        public IInventoryService<T> GetInventoryService<T>() where T : class
        {
            IRepositoryBase<T> repositoryBase = _repositoryBaseFactory.CreateRepositoryBase<T>();
            return new InventoryService<T>(repositoryBase);
        }
    }
}
