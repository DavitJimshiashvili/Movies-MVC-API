using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MovieManagement.Services.Models.JWT;
using System.Text;

namespace Movies.ItAcademy.API.Infrastructure.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTConfiguration>(configuration.GetSection(nameof(JWTConfiguration)));

            var key = Encoding.ASCII.GetBytes(configuration.GetSection(nameof(JWTConfiguration)).GetSection("SecretKey").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "localhost",
                        ValidAudience = "localhost"
                    };
                });
        }
    }
}
