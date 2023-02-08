using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Filters;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.CRUD
{
    public class GetAllUsuariosRequest : RequestParameter, IRequest<PagedResponse<IEnumerable<UsuarioResponse>>>
    {
    }
}
