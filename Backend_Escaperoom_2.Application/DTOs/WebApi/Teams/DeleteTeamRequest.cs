using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Teams
{
    public class DeleteTeamRequest : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
}
