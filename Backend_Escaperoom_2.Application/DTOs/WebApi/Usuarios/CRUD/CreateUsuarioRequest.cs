using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD
{
    public class CreateUsuarioRequest : IRequest<Response<string>>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<string> Roles { get; set; }

        [SwaggerIgnore]
        [JsonIgnore]
        public string Origin { get; set; }
    }
}
