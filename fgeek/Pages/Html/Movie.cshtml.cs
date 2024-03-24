using fgeek.Entities;
using fgeek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;

namespace fgeek.Pages.Html
{
    public class MovieModel : PageModel
    {
        private readonly ILogger logger;
        private readonly IMovieService movieService;
        public bool IsAuthenticated { get; private set; } = false;
        public string CurrentUsername { get; private set; } = string.Empty;
        public string CurrentId { get; private set; } = string.Empty;
        public Movie? Movie { get; private set; } = null;

        public MovieModel(ILoggerFactory loggerFactory, IMovieService searchingService)
        {
            this.logger = loggerFactory.CreateLogger<MovieModel>();
            this.movieService = searchingService;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            var identity = HttpContext.User.Identity;
            IsAuthenticated = (identity is not null) && identity.IsAuthenticated;

            if (IsAuthenticated)
            {
                CurrentId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                CurrentUsername = identity!.Name!;
            }

            if (id is not null)
            {
                Movie = await movieService.MovieAsync(id);

                if (Movie is null)
                {
                    // Not Found Movie
                    return Page();
                }

                logger.LogInformation
                (
                    "[{DateTime}] - User '{Username}' On Movie '{Name}'",
                    DateTime.Now,
                    CurrentUsername,
                    Movie.Title
                );
            }

            return Page();
        }

        public string Genres()
        {
            var genres = movieService.Genres(Movie!.Id).Result.
                         Select(item => item.Name);

            return string.Join(", ", genres);
        }

        public string ProductionCountries()
        {
            var countries = movieService.ProductionCountries(Movie!.Id).Result.
                            Select(item => item.Name);

            return string.Join(", ", countries);
        }

        public string Crew(string department)
        {
            var members = movieService.Crew(Movie!.Id, department).
                          Result.Select(item => item.Name);

            return string.Join(", ", members);
        }

        public IEnumerable<Cast> Cast()
        {
            return movieService.Cast(Movie!.Id).Result.Take(5);
        }

        public IEnumerable<Video> Video()
        {
            return movieService.Video(Movie!.Id).Result;
        }
    }
}
