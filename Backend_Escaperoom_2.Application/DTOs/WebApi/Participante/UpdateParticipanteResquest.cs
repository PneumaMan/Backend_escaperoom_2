using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Participante
{
    public class UpdateParticipanteResquest : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public long? Telefono { get; set; }

        public int Estado { get; set; }

        public int EscapeRoomId { get; set; }

    }
}
