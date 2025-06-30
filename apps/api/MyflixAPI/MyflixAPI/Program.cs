using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;
using MyflixAPI.Services;
using MyflixAPI.TmdbServices;
using TMDbLib.Client;

namespace MyflixAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5000);
            });

            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<AzureTranslatorService>();

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<MyflixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyflixContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllers();

            builder.Services.AddScoped<TmdbService>();
            builder.Services.AddScoped<MovieTranslationService>();
            builder.Services.AddScoped<ActorTranslationService>();
            builder.Services.AddScoped<GenreTranslationService>();
            builder.Services.AddScoped<MAGTranslationService>();
            builder.Services.AddScoped<ActorService>();
            builder.Services.AddScoped<GenreService>();
            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<RatingService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<WatchlistService>();


            string tmdbApiKey = builder.Configuration["TMDb:ApiKey"]!;

            builder.Services.AddSingleton(provider =>
            {
                var client = new TMDbClient(tmdbApiKey);
                return client;
            });

            var app = builder.Build();

            app.UseRouting();

            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
