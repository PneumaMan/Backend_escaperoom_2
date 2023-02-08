using FluentValidation;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.Usuario
{
    public class ChangedPasswordValidation : AbstractValidator<ChangedPasswordRequest>
    {
        private readonly IUsuarioRepositoryAsync _usuariosRepository;
        private readonly LanguagesHelper _languagesHelper;
        private readonly string _expRegularEmail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        private readonly string _expRegularContraseña = @"^(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$";

        public ChangedPasswordValidation(IUsuarioRepositoryAsync usuariosRepository, LanguagesHelper languagesHelper)
        {
            _usuariosRepository = usuariosRepository;
            _languagesHelper = languagesHelper;

            RuleFor(l => l.Email)
                .NotNull().WithMessage(this._languagesHelper.EmailNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.EmailNullEmpty)
                .Matches(this._expRegularEmail).WithMessage(this._languagesHelper.EmailIncorrecto);

            RuleFor(l => l.OldPassword)
                .NotEmpty().WithMessage(this._languagesHelper.ChangedOldPassNull)
                .NotNull().WithMessage(this._languagesHelper.ChangedOldPassNull)
                .Length(8, 20).WithMessage(this._languagesHelper.ChangedOldPassLenght);

            RuleFor(l => l.NewPassword)
                .NotEmpty().WithMessage(this._languagesHelper.ChangedNewPassNull)
                .NotNull().WithMessage(this._languagesHelper.ChangedNewPassNull)
                .Equal(l => l.NewConfirmPassword).WithMessage(this._languagesHelper.ChangedNewPassEquals)
                .Length(8, 20).WithMessage(this._languagesHelper.ChangedNewPassLenght)
                .Matches(this._expRegularContraseña).WithMessage(this._languagesHelper.PassPoliticaSeguridad);

            RuleFor(l => l.NewConfirmPassword)
                .NotEmpty().WithMessage(this._languagesHelper.ChangedConfirmNewPassNull)
                .NotNull().WithMessage(this._languagesHelper.ChangedConfirmNewPassNull)
                .Equal(l => l.NewPassword).WithMessage(this._languagesHelper.ChangedNewPassEquals)
                .Length(8, 20).WithMessage(this._languagesHelper.ChangedConfirmNewPassLength)
                .Matches(this._expRegularContraseña).WithMessage(this._languagesHelper.PassPoliticaSeguridad);
        }
    }
}
