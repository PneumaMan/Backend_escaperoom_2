using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.Usuarios.Commands
{
    public class AuthenticateUsuarioCommand : IRequestHandler<AuthenticationRequest, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;
        private readonly IUsuarioRepositoryAsync _usuarioRepository;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public AuthenticateUsuarioCommand(IAccountService accountService, IUsuarioRepositoryAsync usuarioRepository,
            LanguagesHelper languagesHelper)
        {
            _accountService = accountService;
            _usuarioRepository = usuarioRepository;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<AuthenticationResponse>> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            //Usuario
            var user = await _usuarioRepository.GetUserByEmailFullAsync(request.Email);

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

            var res = await _accountService.ValidatePasswordAsync(user.Email, request.Password);
            if (!res.Succeeded)
            {
                _errors.Add(new ValidationFailureResponse("Password", this._languagesHelper.LoginPassIncorrect));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            return await _accountService.AuthenticateAsync(user, request.IpAddress);
        }
    }
}
