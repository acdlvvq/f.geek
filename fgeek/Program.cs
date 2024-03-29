using Microsoft.Extensions.FileProviders;
using fgeek.Services;
using fgeek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace fgeek
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                             AddCookie(options => options.LoginPath = "/login");
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<IDatabaseService, DatabaseService>().
                             AddScoped<IAccountService, AccountService>().
                             AddScoped<IMovieService, MovieService>();
            builder.Services.AddRazorPages
            (
                (options) => 
                {
                    options.RootDirectory = "/Pages/Html";
                    options.Conventions.
                        ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
                }
            );

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider
                (
                    Path.Combine(Directory.GetCurrentDirectory(),
                    @"Pages/Assets")
                ),
                RequestPath = new PathString("/Assets")
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}
