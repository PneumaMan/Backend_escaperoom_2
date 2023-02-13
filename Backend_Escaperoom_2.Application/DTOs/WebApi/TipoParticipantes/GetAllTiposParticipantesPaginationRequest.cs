using MediatR;
using Backend_Escaperoom_2.Application.Filters;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class GetAllTiposParticipantesPaginationRequest : RequestParameter, IRequest<PagedResponse<IEnumerable<TipoParticipanteResponse>>>
    {
        //Condiciones de busqueda
        public int EscapeRoomId { get; set; }

        public string NombreTipoParticipante { get; set; }

    }
}
