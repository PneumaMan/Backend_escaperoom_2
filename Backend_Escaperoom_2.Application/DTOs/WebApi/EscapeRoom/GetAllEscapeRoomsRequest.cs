using MediatR;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom
{
    public class GetAllEscapeRoomsRequest : IRequest<Response<IEnumerable<EscapeRoomResponse>>>
    {
    }
}
