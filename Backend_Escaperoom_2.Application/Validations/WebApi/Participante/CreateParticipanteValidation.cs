using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.Participante
{
    public class CreateParticipanteValidation : AbstractValidator<CreateParticipanteResquest>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        public CreateParticipanteValidation(IParticipantesRepositoryAsync participantesRepositoryAsync, IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;

            RuleFor(d => d.Identificacion)
                .NotEmpty().WithMessage("Digite la 'Identificación' del participante.")
                .NotNull().WithMessage("Digite la 'Identificación' del participante.")
                .Length(5, 30).WithMessage("La 'Identificación' del participante de ser entre 5 a 30 caracteres.")
                .Matches(@"^[A-Z0-9]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, 0-9")
                .MustAsync(async (model, ident, cancellation) =>
                {
                    return await IsExistIdentParticipanteAsync(ident, model.EscapeRoomId, cancellation);
                }).WithMessage(this._languagesHelper.ParticipanteExiste);

            RuleFor(d => d.Nombres)
                .NotEmpty().WithMessage("Digite los 'Nombres' del participante.")
                .NotNull().WithMessage("Digite los 'Nombres' del participante.")
                .Length(3, 60).WithMessage("Los 'Nombres' del participante debe tener entre 3 a 60 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z");

            RuleFor(d => d.Apellidos)
                .NotEmpty().WithMessage("Digite los 'Apellidos' del participante.")
                .NotNull().WithMessage("Digite los 'Apellidos' del participante.")
                .Length(3, 60).WithMessage("Los 'Apellidos' del participante debe tener entre 3 a 60 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z");

            When(x => x.Telefono != null, () =>
            {
                RuleFor(p => p.Telefono).Cascade(CascadeMode.Stop)
                    .GreaterThan(0).WithMessage("El 'Telefono' debe ser mayor a 0.");
            });

            RuleFor(p => p.Estado).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Selecione el 'Estado' del participante.")
                .MustAsync(IsExisteEstadoParticipante).WithMessage(this._languagesHelper.EstadoParticipanteNoExiste);

            RuleFor(p => p.EscapeRoomId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Seleccione el 'Escape Room' para el reto.")
                .MustAsync(IsExistEscapeRoomAsync).WithMessage(this._languagesHelper.EscapeRoomNoExiste);
        }

        private async Task<bool> IsExistEscapeRoomAsync(int escapeRoomId, CancellationToken cancellationToken)
        {
            return await _escapeRoomsRepositoryAsync.ExistElementAsync(x => x.Id.Equals(escapeRoomId));
        }

        private async Task<bool> IsExistIdentParticipanteAsync(string identificacion, int idEscapeRoom, CancellationToken cancellationToken)
        {
            return await _participantesRepositoryAsync.IsExistAttributeAsync(e => !e.Identificacion.ToLower().Equals(identificacion.ToLower()), x => x.EscapeRoomId == idEscapeRoom);
        }

        private async Task<bool> IsExisteEstadoParticipante(int estado, CancellationToken cancellationToken)
        {
            return Enum.IsDefined(typeof(EstadosParticipantes), estado);
        }
    }
}
