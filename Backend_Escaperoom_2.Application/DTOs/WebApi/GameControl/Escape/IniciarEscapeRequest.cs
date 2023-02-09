using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.GameControl.Escape
{
    public class IniciarEscapeRequest : IRequest<Response<int>>
    {
        public int EscapeRoomId { get; set; }

    }
}
