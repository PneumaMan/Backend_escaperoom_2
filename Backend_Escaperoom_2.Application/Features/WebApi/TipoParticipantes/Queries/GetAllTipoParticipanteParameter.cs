using Backend_Escaperoom_2.Application.Filters;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries
{
    public class GetAllTipoParticipanteParameter : RequestParameter
    {
        public string EscapeRoomId { get; set; }
        public string NombreTipoParticipante { get; set; }
    }
}
