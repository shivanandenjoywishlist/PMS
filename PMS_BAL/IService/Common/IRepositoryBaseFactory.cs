using PMS_DAL.IRepositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Common
{
    public interface IRepositoryBaseFactory
    {
        IRepositoryBase<T> CreateRepositoryBase<T>() where T : class;
    }
}
