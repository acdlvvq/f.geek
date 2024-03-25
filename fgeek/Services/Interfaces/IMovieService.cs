using fgeek.Entities;

namespace fgeek.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<Movie?> MovieAsync(string id);
        public Task<IEnumerable<Genre>> GenresAsync(string id);
        public Task<IEnumerable<Country>> ProductionCountriesAsync(string id);
        public Task<IEnumerable<Crew>> CrewAsync(string id, string department);
        public Task<IEnumerable<Cast>> CastAsync(string id);
        public Task<IEnumerable<Video>> VideoAsync(string id);
        public Task<IEnumerable<Movie>> GetMoviesAsync(string request);
        public Task UpdateMovieAsync(Movie movie);
    }
}
