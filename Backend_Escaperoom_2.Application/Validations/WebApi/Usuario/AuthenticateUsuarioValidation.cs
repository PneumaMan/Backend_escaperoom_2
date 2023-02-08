using FluentValidation;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.Usuario
{
    public class AuthenticateUsuarioValidation : AbstractValidator<AuthenticationRequest>
    {
        /*Atributos*/
        private readonly IAccountService _usuarioService;
        private readonly LanguagesHelper _languagesHelper;
        private readonly string _expRegularEmail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        private readonly string _expRegularContraseña = @"^(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$";

        public AuthenticateUsuarioValidation(IAccountService usuarioService, LanguagesHelper languagesHelper)
        {
            _usuarioService = usuarioService;
            _languagesHelper = languagesHelper;

            RuleFor(l => l.Email)
                .NotNull().WithMessage(this._languagesHelper.EmailNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.EmailNullEmpty)
                .Matches(this._expRegularEmail).WithMessage(this._languagesHelper.EmailIncorrecto);

            RuleFor(l => l.Password)
                .NotNull().WithMessage(this._languagesHelper.PassNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.PassNullEmpty)
                .Length(8, 20).WithMessage(this._languagesHelper.PassLenght)
                .Matches(this._expRegularContraseña).WithMessage(this._languagesHelper.PassPoliticaSeguridad);
        }
    }
}
