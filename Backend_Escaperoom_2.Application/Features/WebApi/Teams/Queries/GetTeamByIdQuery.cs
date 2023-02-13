using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Teams.Queries
{
    public class GetTeamByIdQuery : IRequestHandler<GetTeamByIdRequest, Response<TeamResponse>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public GetTeamByIdQuery(ITeamsRepositoryAsync TeamsRepositoryAsync, LanguagesHelper languagesHelper,
            IMapper mapper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }
        public async Task<Response<TeamResponse>> Handle(GetTeamByIdRequest request, CancellationToken cancellationToken)
        {
            var team = await _TeamsRepositoryAsync.GetFullTeam(request.Id);
            if (team == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Equipo' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return new Response<TeamResponse>(this._mapper.Map<TeamResponse>(team));
        }
    }
}
