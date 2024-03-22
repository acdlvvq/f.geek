using fgeek.Entities;

namespace fgeek.Services.Interfaces
{
    public interface ISearchingService
    {
        public Task<IEnumerable<Movie>> GetMoviesAsync(string request);
    }
}
