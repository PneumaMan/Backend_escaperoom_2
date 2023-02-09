using Backend_escaperoom_2.Application.DTOs.WebApi.GameControl.Participante;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class EscaparResponse
    {
        public IEnumerable<LlavesResponse> LlavesRespuestas { get; set; }

    }
}
