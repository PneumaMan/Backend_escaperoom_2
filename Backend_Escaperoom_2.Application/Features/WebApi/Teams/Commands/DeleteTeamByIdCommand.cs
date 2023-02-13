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

namespace Backend_Escaperoom_2.Application.Features.WebApi.Teams.Commands
{
    public class DeleteTeamByIdCommand : IRequestHandler<DeleteTeamRequest, Response<int>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public DeleteTeamByIdCommand(ITeamsRepositoryAsync TeamsRepositoryAsync, LanguagesHelper languagesHelper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
        {
            var Team = await _TeamsRepositoryAsync.GetByIdAsync(request.Id);
            if (Team == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Equipo' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            await _TeamsRepositoryAsync.DeleteAsync(Team);
            return new Response<int>(Team.Id, this._languagesHelper.SeHaEliminado);
        }
    }
}
