using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries
{
    public class GetAllEscapeRoomQuery : IRequestHandler<GetAllEscapeRoomsRequest, Response<IEnumerable<EscapeRoomResponse>>>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetAllEscapeRoomQuery(IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<IEnumerable<EscapeRoomResponse>>> Handle(GetAllEscapeRoomsRequest request, CancellationToken cancellationToken)
        {
            var res = await _escapeRoomsRepositoryAsync.GetAllFullEscapeRooms();
            return new Response<IEnumerable<EscapeRoomResponse>>(this._mapper.Map<IEnumerable<EscapeRoomResponse>>(res));
        }
    }
}
