﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Volvo.CrudTruck.Application.Auth
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; } = 86400;
        public int FinalExpiration { get; set; }
    }
}
