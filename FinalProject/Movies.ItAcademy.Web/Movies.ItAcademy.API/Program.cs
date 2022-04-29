using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieManagement.Domain.POCO;
using MovieManagement.PresistentDB.Context;
using MovieManagement.PresistentDB.Seed;
using System.Threading.Tasks;

namespace Movies.ItAcademy.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {

                try
                {
                    var services = scope.ServiceProvider;
                    var database = services.GetRequiredService<MovieManagementContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await MovieManagementSeed.Initialize(database, userManager, roleManager);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
