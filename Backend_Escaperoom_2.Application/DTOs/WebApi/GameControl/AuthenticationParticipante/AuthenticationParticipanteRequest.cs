using MediatR;
using Newtonsoft.Json;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.AuthenticationParticipante
{
    public class AuthenticationParticipanteRequest : IRequest<Response<AuthenticationParticipanteResponse>>
    {
        public string Identificacion { get; set; }

        public string EscapeRoomId { get; set; }

        public string RetoId { get; set; }

    }
}
