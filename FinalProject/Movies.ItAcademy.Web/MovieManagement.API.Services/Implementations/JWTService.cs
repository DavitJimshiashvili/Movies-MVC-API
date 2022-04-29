using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieManagement.Services.Abstractions;
using MovieManagement.Services.Models.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Services.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly string _secretKey;
        private readonly int _expDateInMinutes;

        public JWTService(IOptions<JWTConfiguration> options)
        {
            _secretKey = options.Value.SecretKey;
            _expDateInMinutes = options.Value.ExpirationInMinutes;
        }

        public string GenerateJWT(string Id)
        {


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,Id),
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(_expDateInMinutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return  tokenHandler.WriteToken(token);
        }
    }
}
