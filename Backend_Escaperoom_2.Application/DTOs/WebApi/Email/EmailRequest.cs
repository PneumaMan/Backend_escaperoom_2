﻿namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Email
{
    public class EmailRequest
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }
    }
}
