using Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Participante;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class SalidaEscapeRequest : IRequest<Response<EscapeParticipanteResponse>>
    {
        public string ParticipanteId { get; set; }

    }
}
