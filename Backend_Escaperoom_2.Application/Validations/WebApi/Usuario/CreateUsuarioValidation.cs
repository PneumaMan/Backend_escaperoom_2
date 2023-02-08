using FluentValidation;
using Backend_Escaperoom_2.Application.DTOs.Usuarios;
using Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Validations.Usuario
{
    public class CreateUsuarioValidation : AbstractValidator<CreateUsuarioRequest>
    {
        private readonly IUsuarioRepositoryAsync _usuariosRepository;
        private readonly LanguagesHelper _languagesHelper;

        private readonly string _expRegularEmail = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        
        public CreateUsuarioValidation(IUsuarioRepositoryAsync usuariosRepository, LanguagesHelper languagesHelper)
        {
            _usuariosRepository = usuariosRepository;
            _languagesHelper = languagesHelper;

            RuleFor(l => l.Email).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(this._languagesHelper.EmailNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.EmailNullEmpty)
                .Matches(this._expRegularEmail).WithMessage(this._languagesHelper.EmailIncorrecto)
                .MustAsync(ValidarEmailAsync).WithMessage("El 'Email' se encuentra en uso.");

            RuleFor(l => l.Password).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(this._languagesHelper.PassNullEmpty)
                .NotEmpty().WithMessage(this._languagesHelper.PassNullEmpty)
                .Length(8, 20).WithMessage(this._languagesHelper.PassLenght)
                .Equal(l => l.ConfirmPassword).WithMessage(this._languagesHelper.PassNoEquals);

            RuleFor(l => l.ConfirmPassword).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Confirme la 'Contraseña'.")
                .NotNull().WithMessage("Confirme la 'Contraseña'.")
                .Length(8, 20).WithMessage("Confirmar 'Contraseña' debe de tener 8 a 20 caracteres.")
                .Equal(l => l.Password).WithMessage(this._languagesHelper.PassNoEquals);
        }

        /*Metodos*/
        private async Task<bool> ValidarEmailAsync(string email, CancellationToken cancellationToken)
        {
            return !await _usuariosRepository.IsExistAttributeAsync(x => x.Email.ToLower().Equals(email.ToLower()));
        }
    }
}
