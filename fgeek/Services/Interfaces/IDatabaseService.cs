using fgeek.Entities;
using SQLite;

namespace fgeek.Services.Interfaces
{
    public interface IDatabaseService
    {
        public void Open(string path);
        public Task<int> InsertAsync<T>(T item) where T : IEntity;
        public Task<IEnumerable<T>> TableAsync<T>() where T : IEntity, new();
    }   
}
