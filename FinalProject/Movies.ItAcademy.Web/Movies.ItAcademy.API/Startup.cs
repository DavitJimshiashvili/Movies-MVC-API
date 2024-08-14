using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieManagement.API.Services.Extensions;
using MovieManagement.Data.EF;
using MovieManagement.PresistentDB.Extensions;
using Movies.ItAcademy.API.Infrastructure.Extensions;
using Movies.ItAcademy.API.Infrastructure.Mappings;
using Movies.ItAcademy.API.Infrastructure.Middlewares;

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
            services.AddDBContext(Configuration).AddRepository();
            services.AddAppService();
            services.RegisterMaps();
            services.AddTokenAuthentication(Configuration);
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
