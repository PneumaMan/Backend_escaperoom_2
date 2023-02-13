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
    public class DeleteTipoParticipanteByIdCommand : IRequestHandler<DeleteTipoParticipanteRequest, Response<int>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public DeleteTipoParticipanteByIdCommand(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync, LanguagesHelper languagesHelper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(DeleteTipoParticipanteRequest request, CancellationToken cancellationToken)
        {
            var tipoParticipante = await _tipoParticipantesRepositoryAsync.GetByIdAsync(request.Id);
            if (tipoParticipante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", "El 'Tipo Participante' no existe."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            await _tipoParticipantesRepositoryAsync.DeleteAsync(tipoParticipante);
            return new Response<int>(tipoParticipante.Id, this._languagesHelper.SeHaEliminado);
        }
    }
}
