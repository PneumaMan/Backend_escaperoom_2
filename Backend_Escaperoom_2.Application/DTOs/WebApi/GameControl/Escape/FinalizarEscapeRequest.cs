using MediatR;
using Newtonsoft.Json;
using Backend_Escaperoom_2.Application.Assets.Attributes;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class FinalizarEscapeRequest : IRequest<Response<int>>
    {

        public int EscapeRoomId { get; set; }

    }
}
