using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries
{
    public class GetAllTeamsQuery : IRequestHandler<GetAllTeamsRequest, Response<IEnumerable<TeamResponse>>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetAllTeamsQuery(ITeamsRepositoryAsync TeamsRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<IEnumerable<TeamResponse>>> Handle(GetAllTeamsRequest request, CancellationToken cancellationToken)
        {
            if (!String.IsNullOrEmpty(request.EscapeRoomId))
            {
                var res = await _TeamsRepositoryAsync.GetAllFullTeams(x => x.EscapeRoomId == Int32.Parse(request.EscapeRoomId));
                return new Response<IEnumerable<TeamResponse>>(this._mapper.Map<IEnumerable<TeamResponse>>(res));

            }
            else
            {
                var res = await _TeamsRepositoryAsync.GetAllFullTeams();
                return new Response<IEnumerable<TeamResponse>>(this._mapper.Map<IEnumerable<TeamResponse>>(res));
            }
        }
    }
}
