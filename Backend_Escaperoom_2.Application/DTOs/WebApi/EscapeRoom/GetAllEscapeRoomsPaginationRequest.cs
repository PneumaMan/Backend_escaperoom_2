using MediatR;
using Backend_Escaperoom_2.Application.Filters;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom
{
    public class GetAllEscapeRoomsPaginationRequest : RequestParameter, IRequest<PagedResponse<IEnumerable<EscapeRoomResponse>>>
    {
        //Condiciones de busqueda
        public string NombreEscapeRoom { get; set; }

    }
}
