using fgeek.Entities;
using fgeek.Services.Interfaces;

namespace fgeek.Services
{
    public class MovieService : IMovieService
    {
        private readonly IDatabaseService databaseService;
        public MovieService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
            this.databaseService.Open("Movies.db");
        }

        public async Task<Movie?> MovieAsync(string id)
        {
            return (await databaseService.TableAsync<Movie>()).
                    Where(item => item.Id == id).
                    FirstOrDefault();
        }

        public async Task<IEnumerable<Genre>> GenresAsync(string id)
        {
            var genres = ByteConverterService.ToIntCollection
            (
                (await MovieAsync(id))!.GenreIds
            );

            return (await databaseService.TableAsync<Genre>()).
                    Where(item => genres.Contains(item.Id));
        }

        public async Task<IEnumerable<Country>> ProductionCountriesAsync(string id)
        {
            var countries = ByteConverterService.ToIntCollection
            (
                (await MovieAsync(id))!.ProductionCountriesIds
            );

            return (await databaseService.TableAsync<Country>()).
                    Where(item => countries.Contains(item.Id));
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string request)
        {
            return (await databaseService.TableAsync<Movie>()).
                    Where(item => item.Title.Contains
                   (request, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IEnumerable<Crew>> CrewAsync(string id, string department)
        {
            var crew = ByteConverterService.ToIntCollection
            (
                (await MovieAsync(id))!.CrewIds
            );

            return (await databaseService.TableAsync<Crew>()).
                    Where(item => crew.Contains(item.Id)).
                    Where(item => item.Department == department);
        }

        public async Task<IEnumerable<Cast>> CastAsync(string id)
        {
            var cast = ByteConverterService.ToIntCollection
            (
                (await MovieAsync(id))!.CastIds
            );

            return (await databaseService.TableAsync<Cast>()).
                    Where(item => cast.Contains(item.Id));
        }

        public async Task<IEnumerable<Video>> VideoAsync(string id)
        {
            var videos = ByteConverterService.ToIntCollection
            (
                (await MovieAsync(id))!.VideoIds
            );

            return (await databaseService.TableAsync<Video>()).
                    Where(item => videos.Contains(item.Id));
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await databaseService.UpdateAsync(movie);
        }
    }
}
