using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class GetEscaparResponse
    {
        public bool EscaparParticipante { get; set; }

        //public IEnumerable<RespuestaParticipanteResponse> RespuestasParticipante { get; set; }

        public IEnumerable<string> Llaves { get; set; }

    }
}
