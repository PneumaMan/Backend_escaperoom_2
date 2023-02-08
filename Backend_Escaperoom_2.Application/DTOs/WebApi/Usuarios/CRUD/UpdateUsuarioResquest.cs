using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;
using System;

namespace Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD
{
    public class UpdateUsuarioResquest : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public bool EstadoUsuario { get; set; }
    }
}
