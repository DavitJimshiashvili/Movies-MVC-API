using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieManagement.PresistentDB.Context;
using MovieManagement.PresistentDB.Seed;
using MovieManagement.Services.Models.JWT;
using Movies.ItAcademy.API.Infrastructure.Extensions;
using Movies.ItAcademy.API.Infrastructure.Mappings;
using Movies.ItAcademy.API.Infrastructure.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MovieManagement.Domain.POCO;

namespace Movies.ItAcademy.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidation(conf=> conf.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSwagger();
            services.RegisterIdentityService();
            services.AddAppServices();
            services.RegisterMaps();
            services.AddTokenAuthentication(Configuration);
            services.AddDbContext<MovieManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExcptHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
