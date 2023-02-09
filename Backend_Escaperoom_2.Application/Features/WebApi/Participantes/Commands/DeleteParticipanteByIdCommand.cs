using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Commands
{
    public class DeleteParticipanteByIdCommand : IRequestHandler<DeleteParticipanteRequest, Response<int>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public DeleteParticipanteByIdCommand(IParticipantesRepositoryAsync participantesRepositoryAsync, LanguagesHelper languagesHelper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(DeleteParticipanteRequest request, CancellationToken cancellationToken)
        {
            var participante = await _participantesRepositoryAsync.GetParticipanteByIdFullAsync(request.Id);
            if (participante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", this._languagesHelper.ParticipanteNoExiste));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            //validamos que el participante no haya respondido preguntas
            /*if (participante.RespuestasParticipantes.Count > 0)
            {
                throw new ApiException("El 'Participante' no puede ser eliminado por que ha participado en un escape room.");
            }

            //validamos que el participante no haya respondido preguntas
            if (participante.Encuestas.Count > 0)
            {
                throw new ApiException("El 'Participante' no puede ser eliminado por que ha respondido una encuesta.");
            }*/

            await _participantesRepositoryAsync.DeleteAsync(participante);
            return new Response<int>(participante.Id, this._languagesHelper.SeHaEliminado);
        }
    }
}
