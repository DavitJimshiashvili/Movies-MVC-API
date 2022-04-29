using Microsoft.EntityFrameworkCore;
using MovieManagement.Data;
using MovieManagement.Data.EF;
using MovieManagement.Data.EF.Repositories;
using MovieManagement.PresistentDB.Context;
using MyWorkerService;
using Serilog;

public class program
{

    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .UseWindowsService()
        .ConfigureServices(services =>
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IScopedMovieService, ScopedMovieService>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddDbContext<MovieManagementContext>(options => options.UseSqlServer("Server=DESKTOP-FKMBUF1; Database=ITAcademyMovies; Trusted_Connection=True; MultipleActiveResultSets=true")/*, ServiceLifetime.Scoped*/);
        services.AddHostedService<Worker>();
    });
}