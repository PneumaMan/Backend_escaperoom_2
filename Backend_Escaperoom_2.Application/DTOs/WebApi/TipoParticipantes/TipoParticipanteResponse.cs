using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class TipoParticipanteResponse
    {
        public int Id { get; set; }

        public string NombreTipo { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int EscapeRoomId { get; set; }

        public EscapeRoomResponse EscapeRoom { get; set; }

    }
}
