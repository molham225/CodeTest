using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IMongoDbRepository<TEntity,TKey>
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(TEntity obj);
        void Remove(TKey id);
	}
}
