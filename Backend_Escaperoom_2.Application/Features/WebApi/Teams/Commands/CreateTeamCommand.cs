using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Teams.Commands
{
    public class CreateTeamCommand : IRequestHandler<CreateTeamResquest, Response<int>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;
        private List<ValidationFailureResponse> _errors;

        public CreateTeamCommand(ITeamsRepositoryAsync TeamsRepositoryAsync, IMapper mapper,
            LanguagesHelper languagesHelper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _mapper = mapper;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(CreateTeamResquest request, CancellationToken cancellationToken)
        {
            var res = await _TeamsRepositoryAsync.AddAsync(this._mapper.Map<Team>(request));
            return new Response<int>() { IsSuccess = true, Data = res.Id, Message = this._languagesHelper.SeHaGuardado };
        }
    }
}
