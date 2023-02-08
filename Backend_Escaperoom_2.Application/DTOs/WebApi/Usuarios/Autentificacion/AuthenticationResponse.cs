using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsActived { get; set; }
        public bool ChangedPassword { get; set; }
        public string JWToken { get; set; }
        public DateTime ExpireToken { get; set; }
        public string DatosPersonalesId { get; set; }
        public List<string> Roles { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
