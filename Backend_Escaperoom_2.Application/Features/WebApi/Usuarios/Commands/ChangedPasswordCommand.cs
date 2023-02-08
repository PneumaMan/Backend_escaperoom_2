using MediatR;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Usuarios.Commands
{
    public class ChangedPasswordCommand : IRequestHandler<ChangedPasswordRequest, Response<string>>
    {
        private readonly IAccountService _usuarioService;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public ChangedPasswordCommand(IAccountService usuarioService, LanguagesHelper languagesHelper)
        {
            _usuarioService = usuarioService;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<string>> Handle(ChangedPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _usuarioService.GetUserByEmail(request.Email);
            if (user == null)
            {
                _errors.Add(new ValidationFailureResponse("Email", this._languagesHelper.LoginEmailNoExist));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            if (!user.EstadoUsuario)
            {
                _errors.Add(new ValidationFailureResponse("Email", this._languagesHelper.UserNoActivo));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return await _usuarioService.ChangedPassword(user, request.OldPassword, request.NewPassword);
        }
    }
}
