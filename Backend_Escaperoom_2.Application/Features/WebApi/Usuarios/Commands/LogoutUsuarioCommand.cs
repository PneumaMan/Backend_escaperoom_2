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
    public class LogoutUsuarioCommand : IRequestHandler<LogoutRequest, Response<string>>
    {
        private readonly IAccountService _accountService;
        private readonly IUsuarioRepositoryAsync _usuarioRepository;
        private readonly LanguagesHelper _languagesHelper;

        private List<ValidationFailureResponse> _errors;

        public LogoutUsuarioCommand(IAccountService accountService, IUsuarioRepositoryAsync usuarioRepository,
            LanguagesHelper languagesHelper)
        {
            _accountService = accountService;
            _usuarioRepository = usuarioRepository;
            _languagesHelper = languagesHelper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<Response<string>> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            //Usuario
            var user = await _usuarioRepository.GetUserByIdFullAsync(request.Email.ToLower());
            if (user == null)
            {
                _errors.Add(new ValidationFailureResponse("Email", this._languagesHelper.LoginEmailNoExist));
                throw new ValidationException(_errors, this._languagesHelper.ErrorValidation);
            }

            await this._accountService.LogoutAsync();

            return new Response<string>(user.Id, this._languagesHelper.Logout);
        }
    }
}
