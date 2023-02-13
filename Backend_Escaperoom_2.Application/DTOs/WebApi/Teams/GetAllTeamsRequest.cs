using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class GetAllTeamsRequest : IRequest<Response<IEnumerable<TeamResponse>>>
    {
        public string EscapeRoomId { get; set; }
    }
}
