using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class GetParticipanteByIdRequest : IRequest<Response<ParticipanteResponse>>
    {
        public int Id { get; set; }

    }
}
