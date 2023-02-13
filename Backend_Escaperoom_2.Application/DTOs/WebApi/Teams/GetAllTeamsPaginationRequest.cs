using Backend_Escaperoom_2.Application.Filters;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class GetAllTeamsPaginationRequest : RequestParameter, IRequest<PagedResponse<IEnumerable<TeamResponse>>>
    {
        //Condiciones de busqueda
        public int EscapeRoomId { get; set; }

        public string NombreTeam { get; set; }

    }
}
