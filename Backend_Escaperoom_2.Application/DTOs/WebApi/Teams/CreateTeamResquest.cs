using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class CreateTeamResquest : IRequest<Response<int>>
    {
        public string NombreTeam { get; set; }

        public int Capacidad { get; set; }

        public int EscapeRoomId { get; set; }

    }
}
