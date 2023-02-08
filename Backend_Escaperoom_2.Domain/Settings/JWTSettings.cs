using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Escaperoom_2.Domain.Settings
{
    public class JWTSettings
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInHours { get; set; }

        public double CookieExpireTimeInHours { get; set; }

        public double RefreshTokenDurationInDays { get; set; }

        public double TokenProvidersDurationInHours { get; set; }
    }
}
