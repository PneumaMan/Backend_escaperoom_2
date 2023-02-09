using MediatR;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Text.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom
{
    public class CreateEscapeRoomResquest : IRequest<Response<int>>
    {
        public string NombreEscapeRoom { get; set; }

        public int TipoEscape { get; set; }

        public DateTime FechaInicioJuego { get; set; }

        public DateTime FechaFinJuego { get; set; }

        public string Organizador { get; set; }

        public string CelularOrganizador { get; set; }

        public string TiempoLimiteGeneral { get; set; }

        public string TiempoLimiteParticipantes { get; set; }

    }
}
