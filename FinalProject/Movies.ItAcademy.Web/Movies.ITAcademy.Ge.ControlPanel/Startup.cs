using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieManagement.Data;
using MovieManagement.Data.EF;
using MovieManagement.Data.EF.Repositories;
using MovieManagement.Domain.POCO;
using MovieManagement.PresistentDB.Context;
using Movies.ITAcademy.Ge.ControlPanel.Infrastructure;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel
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
            services.AddDbContext<MovieManagementContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IMVCUserService, MVCUserService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserSevice, UserService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddDistributedMemoryCache();

            //services.AddSession(options => {
            //    options.IdleTimeout = TimeSpan.FromMinutes(15);
            //    options.Cookie.IsEssential = true;
            //});

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = true;
                 options.SignIn.RequireConfirmedAccount = false;
                 options.SignIn.RequireConfirmedPhoneNumber = false;
                 options.Stores.MaxLengthForKeys = 128;

             })
                .AddEntityFrameworkStores<MovieManagementContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, AdditionalUserClaimsPrincipalFactory>();
            services.RegisterMaps();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
