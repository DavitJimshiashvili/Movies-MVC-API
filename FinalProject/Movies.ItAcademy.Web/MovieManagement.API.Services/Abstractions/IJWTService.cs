using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Services.Abstractions
{
    public interface IJWTService
    {
        string GenerateJWT(string userId);
    }
}
