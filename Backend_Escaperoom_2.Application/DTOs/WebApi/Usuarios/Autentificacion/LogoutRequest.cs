using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion
{
    public class LogoutRequest : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
