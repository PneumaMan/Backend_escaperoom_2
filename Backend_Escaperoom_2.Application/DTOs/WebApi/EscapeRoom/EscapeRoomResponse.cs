using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom
{
    public class EscapeRoomResponse
    {
        public int Id { get; set; }

        public string NombreEscapeRoom { get; set; }

        public int Estado { get; set; }

        public string EstadoDescription => this.Estado > 0 ? ((EstadosEscapeRoom)Enum.ToObject(typeof(EstadosEscapeRoom), this.Estado)).GetEnumDescription() : String.Empty;

        public int TipoEscape { get; set; }

        public string TipoEscapeDescription => this.TipoEscape > 0 ? ((TiposEscapes)Enum.ToObject(typeof(TiposEscapes), this.TipoEscape)).GetEnumDescription() : String.Empty;

        public DateTime FechaInicioJuego { get; set; }

        public DateTime FechaFinJuego { get; set; }

        public string Organizador { get; set; }

        public string CelularOrganizador { get; set; }

        public string TiempoLimiteGeneral { get; set; }

        public string TiempoLimiteParticipantes { get; set; }

        [JsonIgnore]
        [SwaggerIgnore]
        public string UrlQREscapeId { get; set; }

        public string UrlQR => $"EscapeRoom={this.UrlQREscapeId}";

        //public IEnumerable<ParticipanteResponse> Participantes { get; set; }

        //public IEnumerable<RetoResponse> Retos { get; set; }
    }
}
