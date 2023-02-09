using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom
{
    public class GetEscapeRoomByIdRequest : IRequest<Response<EscapeRoomResponse>>
    {
        public int Id { get; set; }

    }
}
