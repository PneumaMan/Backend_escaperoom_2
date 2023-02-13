using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries
{
    public class GetAllTiposParticipantesQuery : IRequestHandler<GetAllTiposParticipantesRequest, Response<IEnumerable<TipoParticipanteResponse>>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetAllTiposParticipantesQuery(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<IEnumerable<TipoParticipanteResponse>>> Handle(GetAllTiposParticipantesRequest request, CancellationToken cancellationToken)
        {
            if (!String.IsNullOrEmpty(request.EscapeRoomId))
            {
                var res = await _tipoParticipantesRepositoryAsync.GetAllAsync(x => x.EscapeRoomId == Int32.Parse(request.EscapeRoomId));
                return new Response<IEnumerable<TipoParticipanteResponse>>(this._mapper.Map<IEnumerable<TipoParticipanteResponse>>(res));

            }
            else
            {
                var res = await _tipoParticipantesRepositoryAsync.GetAllAsync();
                return new Response<IEnumerable<TipoParticipanteResponse>>(this._mapper.Map<IEnumerable<TipoParticipanteResponse>>(res));
            }
        }
    }
}
