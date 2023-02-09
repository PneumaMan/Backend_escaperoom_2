using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries
{
    public class GetEscapeRoomByIdQuery : IRequestHandler<GetEscapeRoomByIdRequest, Response<EscapeRoomResponse>>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetEscapeRoomByIdQuery(IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }
        public async Task<Response<EscapeRoomResponse>> Handle(GetEscapeRoomByIdRequest request, CancellationToken cancellationToken)
        {
            var escape = await _escapeRoomsRepositoryAsync.GetEscapeRoomByIdFullAsync(request.Id);
            if (escape == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", this._languagesHelper.EscapeRoomNoExiste));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return new Response<EscapeRoomResponse>(this._mapper.Map<EscapeRoomResponse>(escape));
        }
    }
}
