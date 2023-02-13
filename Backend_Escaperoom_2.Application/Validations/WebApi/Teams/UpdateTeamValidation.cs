using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.Teams
{
    public class UpdateTeamValidation : AbstractValidator<UpdateTeamResquest>
    {
        private readonly ITeamsRepositoryAsync _teamsRepositoryAsync;
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        public UpdateTeamValidation(ITeamsRepositoryAsync teamsRepositoryAsync, IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync,
            LanguagesHelper languagesHelper)
        {
            _teamsRepositoryAsync = teamsRepositoryAsync;
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;

            RuleFor(p => p.NombreTeam).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Nombre' del equipo.")
                .NotNull().WithMessage("Digite el 'Nombre' del equipo.")
                .Length(3, 50).WithMessage("El 'Nombre' del equipo debe tener entre 3 a 50 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z0-9]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z, 0-9")
                .MustAsync(IsExistNombreEquipoAsync).WithMessage("El 'Equipo' ya existe.");

            RuleFor(p => p.Capacidad).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Digite la 'Capacidad' del equipo.")
                .GreaterThanOrEqualTo(0).WithMessage("La 'Capacidad' debe ser mayor o igual a 0.")
                .LessThanOrEqualTo(50).WithMessage("La 'Capacidad' del equipo no debe superar los 50 participantes.");

        }

        private async Task<bool> IsExistNombreEquipoAsync(string nombreEquipo, CancellationToken cancellationToken)
        {
            return await _teamsRepositoryAsync.IsExistAttributeAsync(e => !e.NombreTeam.ToLower().Equals(nombreEquipo.ToLower()));
        }

        private async Task<bool> IsExistEscapeRoomAsync(int escapeRoomId, CancellationToken cancellationToken)
        {
            return await _escapeRoomsRepositoryAsync.ExistElementAsync(x => x.Id.Equals(escapeRoomId));
        }
    }
}
