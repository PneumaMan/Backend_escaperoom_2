using MediatR;
using Microsoft.AspNetCore.Identity;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.Usuarios;
using Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend_Escaperoom_2.Application.Interfaces;

namespace Backend_Escaperoom_2.Application.Features.Usuarios.Commands
{
    public class UpdateUsuarioCommand : IRequestHandler<UpdateUsuarioResquest, Response<string>>
    {
        private readonly IAccountService _usuarioService;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public UpdateUsuarioCommand(IAccountService usuarioService, LanguagesHelper languagesHelper)
        {
            _usuarioService = usuarioService;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<string>> Handle(UpdateUsuarioResquest request, CancellationToken cancellationToken)
        {
            var user = await _usuarioService.GetUserById(request.Id);
            if (user == null)
            {
                _errors.Add(new ValidationFailureResponse("Email", this._languagesHelper.LoginEmailNoExist));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            user.EstadoUsuario = request.EstadoUsuario;

            await _usuarioService.UpdateUser(user);
            return new Response<string>(user.Id);
        }
    }
}
