using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;

namespace MyflixAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MyflixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MyflixContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
