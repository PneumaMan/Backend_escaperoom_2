using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Queries
{
    public class GetAllParticipantesQuery : IRequestHandler<GetAllParticipantesRequest, Response<IEnumerable<ParticipanteResponse>>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetAllParticipantesQuery(IParticipantesRepositoryAsync participantesRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<IEnumerable<ParticipanteResponse>>> Handle(GetAllParticipantesRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Participante> res;
            if (request.EscapeRoomId != null)
            {
                res = await _participantesRepositoryAsync.GetAllParticipantesFullAsync(x => x.EscapeRoomId == Convert.ToInt32(request.EscapeRoomId));
            }
            else
            {
                res = await _participantesRepositoryAsync.GetAllParticipantesFullAsync();
            }

            return new Response<IEnumerable<ParticipanteResponse>>(this._mapper.Map<IEnumerable<ParticipanteResponse>>(res));
        }
    }
}
