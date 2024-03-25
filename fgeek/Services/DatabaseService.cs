using SQLite;
using SQLitePCL;
using fgeek.Services.Interfaces;
using fgeek.Entities.Interfaces;
using fgeek.Entities;

namespace fgeek.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection? connection;

        public DatabaseService() { }

        public void Open(string path)
        {
            var databasePath = Path.Combine
            (
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Data\\")),
                path
            );

            connection = new(databasePath);
        }  

        public async Task<int> InsertAsync<T>(T item) where T : IEntity
        {
            return await connection!.InsertAsync(item);
        }

        public async Task<IEnumerable<T>> TableAsync<T>() where T : IEntity, new()
        {
            return await connection!.Table<T>().
                         ToListAsync();
        }

        public async Task<int> UpdateAsync<T>(T item) where T : IEntity
        {
            return await connection!.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync<T>(T item) where T : IEntity
        {
            return await connection!.DeleteAsync(item);
        }
    }
}
