using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Extensions;
using Backend_Escaperoom_2.Application.Features.WebApi.Parameters.Querires;
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
    public class UpdateEscapeRoomCommand : IRequestHandler<UpdateEscapeRoomResquest, Response<int>>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public UpdateEscapeRoomCommand(IEscapeRoomsRepositoryAsync escapeRoomRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _escapeRoomRepositoryAsync = escapeRoomRepositoryAsync;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<int>> Handle(UpdateEscapeRoomResquest request, CancellationToken cancellationToken)
        {
            var escape = await _escapeRoomRepositoryAsync.GetByIdAsync(request.Id);
            if (escape == null)
            {
                _errors.Add(new ValidationFailureResponse("Id", this._languagesHelper.EscapeRoomNoExiste));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            if (!((EstadosEscapeRoom)Enum.ToObject(typeof(EstadosEscapeRoom), escape.Estado) == EstadosEscapeRoom.Activo))
            {
                _errors.Add(new ValidationFailureResponse("Estado", $"El 'Estado' del escape room es diferente a {EstadosEscapeRoom.Activo.GetEnumDescription()}"));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            TimeSpan tiempoEscape;
            try
            {
                tiempoEscape = TimeSpan.Parse(request.TiempoLimiteEscape);
            }
            catch (Exception)
            {
                _errors.Add(new ValidationFailureResponse("TiempoLimiteEscape", "El formato del 'Tiempo' del escape es incorrecto."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            TimeSpan tiempoParticipantes;
            try
            {
                tiempoParticipantes = TimeSpan.Parse(request.TiempoLimiteEscape);
            }
            catch (Exception)
            {
                _errors.Add(new ValidationFailureResponse("tiempoParticipantes", "El formato del 'Tiempo' de los participantes es incorrecto."));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            escape.NombreEscapeRoom = request.NombreEscapeRoom;
            escape.FechaInicioJuego = request.FechaInicioJuego;
            escape.FechaFinJuego = request.FechaFinJuego;
            escape.TipoEscape = request.TipoEscape;
            escape.Organizador = request.Organizador;
            escape.CelularOrganizador = request.CelularOrganizador;
            escape.TiempoLimiteGeneral = tiempoEscape;
            escape.TiempoLimiteParticipantes = tiempoParticipantes;

            await _escapeRoomRepositoryAsync.UpdateAsync(escape);
            return new Response<int>() { IsSuccess = true, Data = escape.Id, Message = this._languagesHelper.SeHaActualizado };
        }
    }
}
