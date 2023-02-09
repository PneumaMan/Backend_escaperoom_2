using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class EscaparEscapeRequest : IRequest<Response<EscaparResponse>>
    {
        public string ParticipanteId { get; set; }
        public List<string> Llaves { get; set; }

    }
}
