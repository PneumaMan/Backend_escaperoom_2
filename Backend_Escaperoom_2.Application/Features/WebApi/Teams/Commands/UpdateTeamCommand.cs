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
    public class UpdateTeamCommand : IRequestHandler<UpdateTeamResquest, Response<int>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public UpdateTeamCommand(ITeamsRepositoryAsync TeamsRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(UpdateTeamResquest request, CancellationToken cancellationToken)
        {
            var team = await _TeamsRepositoryAsync.GetByIdAsync(request.Id);
            if (team == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Equipo' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            team.NombreTeam = request.NombreTeam;
            team.Capacidad = request.Capacidad;

            await _TeamsRepositoryAsync.UpdateAsync(team);
            return new Response<int>() { IsSuccess = true, Data = team.Id, Message = this._languagesHelper.SeHaActualizado };
        }
    }
}
