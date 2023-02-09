using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class GetEscaparEscapeRequest : IRequest<Response<GetEscaparResponse>>
    {
        public string ParticipanteId { get; set; }

    }
}
