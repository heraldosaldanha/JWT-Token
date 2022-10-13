﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JWTToken.Authentication.Models
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int FinalExpiration { get; set; }
    }
}
