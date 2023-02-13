using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_TipoParticipante_2.Application.Validations.WebApi.TiposParticipantes
{
    public class CreateTipoParticipanteValidation : AbstractValidator<CreateTipoParticipanteResquest>
    {
        private readonly ITipoParticipantesRepositoryAsync _TipoParticipantesRepositoryAsync;
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        public CreateTipoParticipanteValidation(ITipoParticipantesRepositoryAsync TipoParticipantesRepositoryAsync, IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _TipoParticipantesRepositoryAsync = TipoParticipantesRepositoryAsync;
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;

            RuleFor(p => p.NombreTipo).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Nombre' tipo del participante.")
                .NotNull().WithMessage("Digite el 'Nombre' tipo del participante.")
                .Length(3, 100).WithMessage("El 'Nombre' del tipo de participante debe tener entre 3 a 100 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z0-9]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z, 0-9")
                .MustAsync(IsExistNombreTipoParAsync).WithMessage("El 'Equipo' ya existe.");

            RuleFor(p => p.Descripcion).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite la 'Descripción' tipo del participante.")
                .NotNull().WithMessage("Digite la 'Descripción' tipo del participante.")
                .Length(3, 200).WithMessage("La 'Descripción' del tipo de participante debe tener entre 3 a 200 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z0-9]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z, 0-9");


            RuleFor(p => p.EscapeRoomId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Seleccione el 'Escape Room' para registrar el tipo de participante.")
                .MustAsync(IsExistEscapeRoomAsync).WithMessage(this._languagesHelper.EscapeRoomNoExiste);

        }

        private async Task<bool> IsExistNombreTipoParAsync(string nombreTipo, CancellationToken cancellationToken)
        {
            return await _TipoParticipantesRepositoryAsync.IsExistAttributeAsync(e => !e.NombreTipo.ToLower().Equals(nombreTipo.ToLower()));
        }

        private async Task<bool> IsExistEscapeRoomAsync(int escapeRoomId, CancellationToken cancellationToken)
        {
            return await _escapeRoomsRepositoryAsync.ExistElementAsync(x => x.Id.Equals(escapeRoomId));
        }
    }
}
