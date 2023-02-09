using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class GetAllParticipantesRequest : IRequest<Response<IEnumerable<ParticipanteResponse>>>
    {
        public string EscapeRoomId { get; set; }
    }
}
