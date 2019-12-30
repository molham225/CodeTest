using CodeTest.Enums;
using CodeTest.Interfaces;
using CodeTest.Model;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeTest.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;
        }

        public async virtual Task<TEntity> Add(TEntity obj)
        {
            ConfigDbSet();
            Context.AddCommand(async () => await DbSet.InsertOneAsync(obj));
            return obj;
        }

        protected void ConfigDbSet()
        {
            DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<TEntity> GetById<TKey>(TKey id)
        {
            ConfigDbSet();
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            ConfigDbSet();
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(PaginationInfo info)
        {
            ConfigDbSet();
            var all = DbSet.Find(Builders<TEntity>.Filter.Empty);
            var pageItems = await all.Skip(info.Skip).Limit(10).ToListAsync();
            return pageItems;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity,bool>> where)
        {
            ConfigDbSet();
            var all = DbSet.Find(where);
            var pageItems = await all.ToListAsync();
            return pageItems;
        }

        public virtual async Task Update(TEntity obj)
        {
            ConfigDbSet();
            Context.AddCommand(async () => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj));
        }

        public virtual async Task Remove<TKey>(TKey id)
        {
            ConfigDbSet();
            Context.AddCommand(async () => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public async Task CreateUniqueIndex(string columnName)
        {
            IndexKeysDefinition<TEntity> keys =
                        Builders<TEntity>.IndexKeys
                            .Ascending(columnName);

            var options = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<TEntity>(keys, options);

            await DbSet.Indexes.CreateOneAsync(indexModel);
        }


        public virtual async Task<IEnumerable<TEntity>> GetFiltered(List<ColumnFilterInfo> filters, PaginationInfo paginationInfo)
        {

            ConfigDbSet();
            FilterDefinition<TEntity> filterDefinition =
                Builders<TEntity>.Filter.Empty;

            foreach (ColumnFilterInfo filter in filters)
            {
                switch (filter.FilterType) {
                    case FilterTypeEnum.Equals:
                        if (filter.EntityName == typeof(TEntity).Name)
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Eq(filter.ColumnName, filter.Value);
                        }
                        else 
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Eq(filter.EntityName + "." + filter.ColumnName, filter.Value);
                        }
                        break;
                    case FilterTypeEnum.GreaterThan:
                        if (filter.EntityName == typeof(TEntity).Name)
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Gt(filter.ColumnName, filter.Value);
                        }
                        else
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Gt(filter.EntityName + "." + filter.ColumnName, filter.Value);
                        }
                        break;
                    case FilterTypeEnum.GreaterThanOrEquals:
                        if (filter.EntityName == typeof(TEntity).Name)
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Gte(filter.ColumnName, filter.Value);
                        }
                        else
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Gte(filter.EntityName + "." + filter.ColumnName, filter.Value);
                        }
                        break;
                    case FilterTypeEnum.LesserThan:
                        if (filter.EntityName == typeof(TEntity).Name)
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Lt(filter.ColumnName, filter.Value);
                        }
                        else
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Lt(filter.EntityName + "." + filter.ColumnName, filter.Value);
                        }
                        break;
                    case FilterTypeEnum.LesserThanOrEquals:
                        if (filter.EntityName == typeof(TEntity).Name)
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Lte(filter.ColumnName, filter.Value);
                        }
                        else
                        {
                            filterDefinition = filterDefinition & Builders<TEntity>.Filter.Lte(filter.EntityName + "." + filter.ColumnName, filter.Value);
                        }
                        break;
                    
                }
            }
            var all = await DbSet.FindAsync(filterDefinition);
            return all.ToList();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async  Task<int> Commit()
        {
            return await Context.SaveChanges();
        }

        public async Task Abort()
        {
            await Context.CancelChanges();
        }
    }
}
