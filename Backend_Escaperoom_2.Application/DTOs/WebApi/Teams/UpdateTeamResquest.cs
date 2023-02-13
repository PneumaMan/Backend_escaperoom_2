using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class UpdateTeamResquest : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public string NombreTeam { get; set; }

        public int Capacidad { get; set; }

        public int EscapeRoomId { get; set; }

    }
}
