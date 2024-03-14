using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace fgeek.Pages.Html
{
    public class IndexModel : PageModel
    {
        private readonly ILogger logger;
        public bool IsAuthenticated { get; private set; } = false;
        public string CurrentUsername { get; private set; } = string.Empty;
        public string CurrentId { get; private set; } = string.Empty;
        public IndexModel(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<IndexModel>();
        }

        public IActionResult OnGet()
        {
            var identity = HttpContext.User.Identity;
            IsAuthenticated = (identity is not null) && (identity.IsAuthenticated);

            if (IsAuthenticated) 
            {
                CurrentId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                CurrentUsername = identity!.Name!;
            }

            return Page();
        }
    }
}
