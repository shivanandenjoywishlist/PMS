using PMS_BAL.IService.Common;
using PMS_BAL.IService.Inventory;
using PMS_BAL.Service.Common;
using PMS_DAL.IRepositories.Common;


namespace PMS_BAL.Service.Inventory
{
    public class InventoryService<T> : BaseService, IInventoryService<T> where T : class
    {
        private readonly IRepositoryBase<T> _repositoryBase;

        public InventoryService(IRepositoryBase<T> repositoryBase)
        {
            _repositoryBase = repositoryBase ?? throw new ArgumentNullException(nameof(repositoryBase));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _repositoryBase.GetAll();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("Error in InventoryService.GetAll()", ex);
            }
        }
    }
}
