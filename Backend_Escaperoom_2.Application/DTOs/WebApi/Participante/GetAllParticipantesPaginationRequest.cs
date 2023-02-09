using MediatR;
using Backend_Escaperoom_2.Application.Filters;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class GetAllParticipantesPaginationRequest : RequestParameter, IRequest<PagedResponse<IEnumerable<ParticipanteResponse>>>
    {
        //Condiciones de busqueda
        public int? EscapeRoomId { get; set; }

        public string NombreParticipante { get; set; }
    }
}
