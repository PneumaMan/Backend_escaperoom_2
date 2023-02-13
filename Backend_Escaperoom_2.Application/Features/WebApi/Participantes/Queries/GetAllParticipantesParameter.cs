using Backend_Escaperoom_2.Application.Filters;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Queries
{
    public class GetAllParticipantesParameter : RequestParameter
    {
        public string EscapeRoomId { get; set; }

        public string NombreParticipante { get; set; }
    }
}
