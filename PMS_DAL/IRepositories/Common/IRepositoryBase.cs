using System;
using System.Collections.Generic;

namespace PMS_DAL.IRepositories.Common
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task Create(T entity);
        Task CreateBulk(List<T> entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
        Task Delete(T entity);
    }
}
