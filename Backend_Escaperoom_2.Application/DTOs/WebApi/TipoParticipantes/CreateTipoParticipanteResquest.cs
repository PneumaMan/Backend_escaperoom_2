using MediatR;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Text.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class CreateTipoParticipanteResquest : IRequest<Response<int>>
    {
        public string NombreTipo { get; set; }

        public string Descripcion { get; set; }

        public int EscapeRoomId { get; set; }

    }
}
