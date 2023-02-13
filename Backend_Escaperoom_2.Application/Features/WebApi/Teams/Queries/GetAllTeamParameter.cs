using Backend_Escaperoom_2.Application.Filters;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries
{
    public class GetAllTeamParameter : RequestParameter
    {
        public string EscapeRoomId { get; set; }
        public string NombreTeam { get; set; }
    }
}
