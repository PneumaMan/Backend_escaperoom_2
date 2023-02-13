using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Commands
{
    public class UpdateTipoParticipanteCommand : IRequestHandler<UpdateTipoParticipanteResquest, Response<int>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public UpdateTipoParticipanteCommand(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(UpdateTipoParticipanteResquest request, CancellationToken cancellationToken)
        {
            var tipoParticipante = await _tipoParticipantesRepositoryAsync.GetByIdAsync(request.Id);
            if (tipoParticipante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Tipo Participante' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            tipoParticipante.NombreTipo = request.NombreTipo;
            tipoParticipante.Descripcion = request.Descripcion;
            tipoParticipante.Estado = request.Estado;

            await _tipoParticipantesRepositoryAsync.UpdateAsync(tipoParticipante);
            return new Response<int>() { IsSuccess = true, Data = tipoParticipante.Id, Message = this._languagesHelper.SeHaActualizado };
        }
    }
}
