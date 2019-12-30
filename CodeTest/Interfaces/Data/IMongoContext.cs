using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CodeTest.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        Task CancelChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}