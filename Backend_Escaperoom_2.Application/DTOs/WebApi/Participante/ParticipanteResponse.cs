using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Extensions;
using System;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class ParticipanteResponse
    {
        public int Id { get; set; }

        public int TipoIdentificacion { get; set; }

        public string TipoDescription => this.TipoIdentificacion > 0 ? ((EstadosParticipantes)Enum.ToObject(typeof(EstadosParticipantes), this.TipoIdentificacion)).GetEnumDescription() : String.Empty;

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string FullName => $"{this.Nombres} {this.Apellidos}";

        public long? Telefono { get; set; }

        public int Estado { get; set; }

        public string EstadoDescription => this.Estado > 0 ? ((EstadosParticipantes)Enum.ToObject(typeof(EstadosParticipantes), this.Estado)).GetEnumDescription() : String.Empty;

        public string TimeScore { get; set; }

        public int EscapeRoomId { get; set; }

        public EscapeRoomResponse EscapeRoom { get; set; }

        public int TipoParticipanteId { get; set; }

        public TipoParticipanteResponse TipoParticipante { get; set; }

        public int? TeamId { get; set; }

        public TeamResponse MyTeam { get; set; }

        //public IEnumerable<RespuestaParticipanteResponse> RespuestasParticipantes { get; set; }

        //public IEnumerable<EncuestaResponse> Encuestas { get; set; }
    }

}
