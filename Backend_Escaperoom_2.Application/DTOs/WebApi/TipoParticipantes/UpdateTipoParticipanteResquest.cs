using MediatR;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Text.Json.Serialization;
using System;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class UpdateTipoParticipanteResquest : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public string NombreTipo { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int EscapeRoomId { get; set; }

    }
}
