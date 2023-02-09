using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Commands
{
    public class CreateEscapeRoomCommand : IRequestHandler<CreateEscapeRoomResquest, Response<int>>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;
        private List<ValidationFailureResponse> _errors;

        public CreateEscapeRoomCommand(IEscapeRoomsRepositoryAsync escapeRoomRepositoryAsync, IMapper mapper,
            LanguagesHelper languagesHelper)
        {
            _escapeRoomRepositoryAsync = escapeRoomRepositoryAsync;
            _mapper = mapper;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(CreateEscapeRoomResquest request, CancellationToken cancellationToken)
        {
            var escape = this._mapper.Map<Domain.Entities.EscapeRoom>(request);

            TimeSpan tiempoEscape;
            try
            {
                tiempoEscape = TimeSpan.Parse(request.TiempoLimiteGeneral);
            }
            catch (Exception)
            {
                _errors.Add(new ValidationFailureResponse("TiempoLimiteGeneral", "El formato del 'Tiempo' del escape es incorrecto."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            TimeSpan tiempoParticipantes;
            try
            {
                tiempoParticipantes = TimeSpan.Parse(request.TiempoLimiteParticipantes);
            }
            catch (Exception)
            {
                _errors.Add(new ValidationFailureResponse("TiempoLimiteParticipantes", "El formato del 'Tiempo' de los participantes es incorrecto."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            escape.Estado = (int)EstadosEscapeRoom.Activo;
            escape.TiempoLimiteGeneral = tiempoEscape;
            escape.TiempoLimiteParticipantes = tiempoParticipantes;

            var res = await _escapeRoomRepositoryAsync.AddAsync(escape);
            return new Response<int>() { IsSuccess = true, Data = res.Id, Message = this._languagesHelper.SeHaGuardado };
        }
    }
}
