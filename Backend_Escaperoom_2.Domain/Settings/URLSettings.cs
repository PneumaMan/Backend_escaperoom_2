using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Escaperoom_2.Domain.Settings
{
    public class URLSettings
    {
        public bool UseBaseUrl { get; set; }

        public string BaseUrl { get; set; }

        public string ConfirmEmailUrl { get; set; }

        public string ForgotPasswordlUrl { get; set; }
    }
}
