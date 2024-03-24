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
                    Where(item => item.Id == id)
                    .FirstOrDefault();
        }

        public async Task<IEnumerable<Genre>> Genres(string id)
        {
            var movieGenresBytes = (await MovieAsync(id))!.GenreIds;
            var movieGenres = new int[movieGenresBytes.Length / sizeof(int)];
            Buffer.BlockCopy(movieGenresBytes, 0, movieGenres, 0, movieGenresBytes.Length);

            return (await databaseService.TableAsync<Genre>()).
                    Where(item => movieGenres.Contains(item.Id));
        }

        public async Task<IEnumerable<Country>> ProductionCountries(string id)
        {
            var countriesBytes = (await MovieAsync(id))!.
                                  ProductionCountriesIds;
            var countries = new int[countriesBytes.Length / sizeof(int)];
            Buffer.BlockCopy(countriesBytes, 0, countries, 0, countriesBytes.Length);

            return (await databaseService.TableAsync<Country>()).
                    Where(item => countries.Contains(item.Id));
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string request)
        {
            return (await databaseService.TableAsync<Movie>()).
                    Where(item => item.Title.Contains(request, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IEnumerable<Crew>> Crew(string id, string department)
        {
            var crewByte = (await MovieAsync(id))!.CrewIds;
            var crew = new int[crewByte.Length / sizeof(int)];
            Buffer.BlockCopy(crewByte, 0, crew, 0, crewByte.Length);

            return (await databaseService.TableAsync<Crew>()).
                    Where(item => crew.Contains(item.Id)).
                    Where(item => item.Department == department);
        }

        public async Task<IEnumerable<Cast>> Cast(string id)
        {
            var castByte = (await MovieAsync(id))!.CastIds;
            var cast = new int[castByte.Length / sizeof(int)];
            Buffer.BlockCopy(castByte, 0, cast, 0, castByte.Length);

            return (await databaseService.TableAsync<Cast>()).
                    Where(item => cast.Contains(item.Id));
        }

        public async Task<IEnumerable<Video>> Video(string id)
        {
            var videosByte = (await MovieAsync(id))!.VideoIds;
            var videos = new int[videosByte.Length / sizeof(int)];
            Buffer.BlockCopy(videosByte, 0, videos, 0, videosByte.Length);

            return (await databaseService.TableAsync<Video>()).
                    Where(item => videos.Contains(item.Id));
        }
    }
}
