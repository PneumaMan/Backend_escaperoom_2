using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class TipoParticipanteFullResponse
    {
        public int Id { get; set; }

        public string NombreTipo { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int EscapeRoomId { get; set; }

        public EscapeRoomResponse EscapeRoom { get; set; }

        public IEnumerable<ParticipanteResponse> Participantes { get; set; }

        //public IEnumerable<Reto> Retos { get; set; }
    }
}
