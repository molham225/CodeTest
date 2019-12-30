using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> GetById<TKey>(TKey id);

        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(PaginationInfo paginationInfo);
        Task Update(TEntity obj);
        Task Remove<TKey>(TKey id);
        Task<IEnumerable<TEntity>> GetFiltered(List<ColumnFilterInfo> filters, PaginationInfo paginationInfo);
        Task<int> Commit();
        Task Abort();
        Task CreateUniqueIndex(string columnName);
    }
}
