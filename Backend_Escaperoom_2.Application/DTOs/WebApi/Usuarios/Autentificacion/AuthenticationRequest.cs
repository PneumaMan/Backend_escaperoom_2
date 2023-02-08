using MediatR;
using Newtonsoft.Json;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion
{
    public class AuthenticationRequest : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        [SwaggerIgnore]
        public string IpAddress { get; set; }

    }
}
