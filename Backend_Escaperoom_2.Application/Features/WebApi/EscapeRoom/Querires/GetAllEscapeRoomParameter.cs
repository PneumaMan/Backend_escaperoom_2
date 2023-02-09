using Backend_Escaperoom_2.Application.Filters;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries
{
    public class GetAllEscapeRoomParameter : RequestParameter
    {
        public string NombreEscapeRoom { get; set; }
    }
}
