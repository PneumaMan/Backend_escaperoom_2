using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes
{
    public class GetTipoParticipanteByIdRequest : IRequest<Response<TipoParticipanteResponse>>
    {
        public int Id { get; set; }

    }
}
