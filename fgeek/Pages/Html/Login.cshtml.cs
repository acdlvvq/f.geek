using fgeek.Entities;
using fgeek.Exceptions;
using fgeek.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;

namespace fgeek.Pages.Html
{
    public class LoginModel : PageModel
    {
        private readonly ILogger logger;
        private readonly IAccountService accountService;
        public string ErrorMessage { get; private set; } = string.Empty;
        public LoginModel(IAccountService accountService, ILoggerFactory loggerFactory)
        {
            this.accountService = accountService;
            this.logger = loggerFactory.CreateLogger<LoginModel>();
        }

        private async Task<IActionResult> RegistrationHandler(string? registrationEmail,
                                                              string? registrationUsername,
                                                              string? registrationPassword)
        {
            try
            {
                if (registrationEmail is null ||
                    registrationUsername is null ||
                    registrationPassword is null)
                {
                    ErrorMessage = string.Empty;
                    throw new Exception("Wrong Format Request");
                }

                var user = new User(string.Empty,
                                    registrationEmail,
                                    registrationUsername,
                                    registrationPassword);
                await accountService.CreateAccountAsync(user);
                logger.LogInformation
                 (
                    "[{DateTime}] - Account '{Username}' ({Email}) Created",
                    DateTime.Now,
                    user.Username,
                    user.Email
                 );

                var claims = new List<Claim>
                    {
                        new (ClaimTypes.NameIdentifier, user.Id),
                        new (ClaimTypes.Name, user.Username)
                    };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync
                (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new(claimsIdentity)
                );
                return new LocalRedirectResult("/");
            }
            catch (Exception ex)
            {
                if (ex is FieldAlreadyTakenException)
                {
                    logger.LogError
                    (
                        "[{DateTime}] - Failed To Create Account: {ErrorMessage}",
                        DateTime.Now,
                        ex.Message
                    );
                    ErrorMessage = ex.Message;
                }
                return Page();
            }
        }

        private async Task<IActionResult> AuthorizationHandler(string? authorizationEmail,
                                                               string? authorizationPassword)
        {
            try
            {
                if (authorizationEmail is null ||
                    authorizationPassword is null)
                {
                    ErrorMessage = string.Empty;
                    throw new Exception("Wrong Format Request");
                }

                var authorizationRequest = 
                    new AuthorizationRequest(authorizationEmail, authorizationPassword);
                var user = await accountService.LogInAsync(authorizationRequest);
                logger.LogInformation
                (
                    "[{DateTime}] - User '{Username}' ({Email}) Logged In",
                    DateTime.Now,
                    user.Username,
                    user.Email
                );

                var claims = new List<Claim>
                {
                    new (ClaimTypes.NameIdentifier, user.Id),
                    new (ClaimTypes.Name, user.Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                await HttpContext.SignInAsync
                (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new(claimsIdentity)
                );
                return new LocalRedirectResult("/");
            }
            catch (Exception ex)
            {
                if (ex is AccountNotFoundException)
                {
                    logger.LogError
                    (
                        "[{DateTime}] - Failed To Log In: {ErrorMessage}",
                        DateTime.Now,
                        ex.Message
                    );
                    ErrorMessage = ex.Message;
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(string? registrationEmail,
                                                    string? registrationUsername,
                                                    string? registrationPassword,
                                                    string? authorizationEmail,
                                                    string? authorizationPassword)
        {
            if (authorizationEmail is null &&
                authorizationPassword is null) 
            {
                return await RegistrationHandler(registrationEmail,
                                                 registrationUsername,
                                                 registrationPassword);
            }
            else
            {
                return await AuthorizationHandler(authorizationEmail,
                                                  authorizationPassword);
            }
        }

    }
}