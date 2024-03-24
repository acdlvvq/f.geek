using fgeek.Entities;

namespace fgeek.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<Movie?> MovieAsync(string id);
        public Task<IEnumerable<Genre>> Genres(string id);
        public Task<IEnumerable<Country>> ProductionCountries(string id);
        public Task<IEnumerable<Crew>> Crew(string id, string department);
        public Task<IEnumerable<Cast>> Cast(string id);
        public Task<IEnumerable<Video>> Video(string id);
        public Task<IEnumerable<Movie>> GetMoviesAsync(string request);
    }
}
