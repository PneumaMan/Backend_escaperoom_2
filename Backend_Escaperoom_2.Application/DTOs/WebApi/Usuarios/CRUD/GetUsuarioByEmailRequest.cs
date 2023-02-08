using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.CRUD
{
    public class GetUsuarioByEmailRequest : IRequest<Response<UsuarioResponse>>
    {
        public string Email { get; set; }
    }
}
