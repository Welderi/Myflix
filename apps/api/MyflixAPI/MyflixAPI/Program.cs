using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyflixAPI.Models;
using MyflixAPI.Services;
using MyflixAPI.TmdbServices;
using System.Text;
using TMDbLib.Client;

namespace MyflixAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DOCKER

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5000);
            });

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // HTTP CLIENT

            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<AzureTranslatorService>();


            // JWT

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetValue<string>("JwtSettings:JwtKey")!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.HttpContext.Request.Cookies["jwt"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // SQL 

            builder.Services.AddDbContext<MyflixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // SERVICES

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MyflixContext>()
                .AddSignInManager();

            builder.Services.AddControllers();

            builder.Services.AddScoped<TmdbService>();
            builder.Services.AddScoped<MovieTranslationService>();
            builder.Services.AddScoped<ActorTranslationService>();
            builder.Services.AddScoped<GenreTranslationService>();
            builder.Services.AddScoped<MAGTranslationService>();
            builder.Services.AddScoped<ActorService>();
            builder.Services.AddSingleton<GenerateJwtTokenService>();
            builder.Services.AddScoped<GenreService>();
            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<RatingService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<WatchlistService>();

            // TMDB

            string tmdbApiKey = builder.Configuration["TMDb:ApiKey"]!;

            builder.Services.AddSingleton(provider =>
            {
                var client = new TMDbClient(tmdbApiKey);
                return client;
            });

            var app = builder.Build();

            app.UseRouting();

            // REACT

            app.UseCors("AllowReactApp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
