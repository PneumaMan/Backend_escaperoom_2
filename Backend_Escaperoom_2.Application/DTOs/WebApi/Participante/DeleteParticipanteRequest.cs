using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class DeleteParticipanteRequest : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
}
