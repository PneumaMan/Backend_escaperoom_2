using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Extensions;
using System;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.AuthenticationParticipante
{
    public class AuthenticationParticipanteResponse
    {
        public string Id { get; set; }

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string FullName => $"{this.Nombres} {this.Apellidos}";

        public long? Telefono { get; set; }

        public int Estado { get; set; }

        public string EstadoDescription => this.Estado > 0 ? ((EstadosParticipantes)Enum.ToObject(typeof(EstadosParticipantes), this.Estado)).GetEnumDescription() : String.Empty;

        public string TiempoRestante { get; set; }

        public string EscapeRoomId { get; set; }

        //public RetoParticipanteResponse NextReto { get; set; }

        //public string NextRetoMessage => this.NextReto != null ? $"Dirigete al Reto {this.NextReto.NumeroReto}" : null;
    }

}
