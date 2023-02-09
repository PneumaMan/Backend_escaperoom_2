using AutoMapper;
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
    public class UpdateParticipanteCommand : IRequestHandler<UpdateParticipanteResquest, Response<int>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public UpdateParticipanteCommand(IParticipantesRepositoryAsync participantesRepositoryAsync,
            IMapper mapper, LanguagesHelper languagesHelper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _mapper = mapper;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(UpdateParticipanteResquest request, CancellationToken cancellationToken)
        {
            var participante = await _participantesRepositoryAsync.GetByIdAsync(request.Id);
            if (participante == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", this._languagesHelper.ParticipanteNoExiste));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            participante.Identificacion = request.Identificacion;
            participante.Nombres = request.Nombres;
            participante.Apellidos = request.Apellidos;
            participante.Telefono = request.Telefono;
            participante.Estado = request.Estado;

            await _participantesRepositoryAsync.UpdateAsync(participante);
            return new Response<int>() { IsSuccess = true, Data = participante.Id, Message = this._languagesHelper.SeHaActualizado };
        }
    }
}
