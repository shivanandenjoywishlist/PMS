using PMS_BAL.IService.Common;
using PMS_DAL.IRepositories.Common;
using PMS_DAL.Repositories.Common;
using PMS_DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.Service.Common
{
    public class RepositoryBaseFactory : IRepositoryBaseFactory
    {
        private readonly ApplicationContext _context; // Assuming ApplicationContext is injected into the factory

        public RepositoryBaseFactory(ApplicationContext context)
        {
            _context = context;
        }

        public IRepositoryBase<T> CreateRepositoryBase<T>() where T : class
        {
            return new RepositoryBase<T>(_context);
        }
    }
}
