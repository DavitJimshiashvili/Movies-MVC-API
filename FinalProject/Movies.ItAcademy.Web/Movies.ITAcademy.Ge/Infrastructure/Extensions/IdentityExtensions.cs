using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.POCO;
using MovieManagement.PresistentDB.Context;

namespace Movies.ITAcademy.Ge.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static void RegisterIdentityService(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options =>
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
        }
    }
}
