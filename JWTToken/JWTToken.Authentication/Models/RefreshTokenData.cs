using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models
{
    public class RefreshTokenData
    {
        public string Access { get; set; }
        public string RefreshToken { get; set; }
    }
}
