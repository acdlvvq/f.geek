using fgeek.Entities;
using fgeek.Services.Interfaces;

namespace fgeek.Services
{
    public class SearchingService : ISearchingService
    {
        private readonly IDatabaseService databaseService;
        public SearchingService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
            this.databaseService.Open("Movies.db");
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string request)
        {
            return (await databaseService.TableAsync<Movie>()).
                    Where(item => item.Title.ToLower().Contains(request.ToLower()));
        }
    }
}
