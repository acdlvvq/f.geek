using SQLite;
using SQLitePCL;
using fgeek.Entities;
using fgeek.Services.Interfaces;

namespace fgeek.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection? connection;

        public DatabaseService() 
        {

        }

        public async Task OpenAsync(string path)
        {
            var databasePath = Path.Combine
            (
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Data\\")),
                path
            );

            connection = new(databasePath);
            await connection.CreateTableAsync<User>();
        }  

        public async Task<int>InsertAsync<T>(T item) where T : IEntity
        {
            return await connection!.InsertAsync(item);
        }

        public async Task<IEnumerable<T>> TableAsync<T>() where T : IEntity, new()
        {
            return await connection!.Table<T>().
                         ToListAsync();
        }
    }
}
