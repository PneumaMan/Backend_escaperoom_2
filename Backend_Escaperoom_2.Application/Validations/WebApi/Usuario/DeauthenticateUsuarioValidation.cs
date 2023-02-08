using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Helpers;
using FluentValidation;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.Usuario
{
    public class DeauthenticateUsuarioValidation : AbstractValidator<LogoutRequest>
    {
        /*Atributos*/
        private readonly LanguagesHelper _languagesHelper;
        private readonly string _expRegularEmail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";

        public DeauthenticateUsuarioValidation(LanguagesHelper languagesHelper)
        {
            _languagesHelper = languagesHelper;

            RuleFor(l => l.Email)
                .NotNull().WithMessage(this._languagesHelper.EmailNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.EmailNullEmpty)
                .Matches(this._expRegularEmail).WithMessage(this._languagesHelper.EmailIncorrecto);
        }
    }
}
