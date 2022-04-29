using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Services.Models.JWT
{
    public class JWTConfiguration
    {
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
