using fgeek.Entities;
using fgeek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace fgeek.Pages.Html
{
    public class SearchModel : PageModel
    {
        private readonly ILogger logger;
        private readonly ISearchingService searchingService;
        public bool IsAuthenticated { get; private set; } = false;
        public string CurrentUsername { get; private set; } = string.Empty;
        public string CurrentId { get; private set; } = string.Empty;
        public IEnumerable<Movie> SearchingResult { get; private set; } = [];

        public SearchModel(ILoggerFactory loggerFactory, ISearchingService searchingService)
        {
            this.logger = loggerFactory.CreateLogger<SearchModel>();
            this.searchingService = searchingService;
        }

        public async Task<IActionResult> OnGetAsync(string? q)
        {
            var identity = HttpContext.User.Identity;
            IsAuthenticated = (identity is not null) && identity.IsAuthenticated;

            if (IsAuthenticated)
            {
                CurrentId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                CurrentUsername = identity!.Name!;
            }

            if (q is not null)
            {
                SearchingResult = await searchingService.GetMoviesAsync(q);
                logger.LogInformation
                (
                    "[{DateTime}] - User '{Username}' : Search Request '{Request}' - Results: {Count}",
                    DateTime.Now,
                    CurrentUsername,
                    q,
                    SearchingResult.Count()
                );
            }

            return Page();
        }
    }
}
