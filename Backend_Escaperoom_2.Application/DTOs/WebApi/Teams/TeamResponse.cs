using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using System;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class TeamResponse
    {
        public int Id { get; set; }

        public string NombreTeam { get; set; }

        public int Estado { get; set; }

        public TimeSpan? TimeScoreTeam { get; set; }

        public int EscapeRoomId { get; set; }
        public EscapeRoomResponse EscapeRoom { get; set; }

        public IEnumerable<ParticipanteResponse> Participantes { get; set; }
    }
}
