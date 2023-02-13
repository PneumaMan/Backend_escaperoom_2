using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Validations.WebApi.EscapeRoom
{
    public class UpdateEscapeRoomValidation : AbstractValidator<UpdateTipoParticipanteResquest>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly LanguagesHelper _languagesHelper;

        public UpdateEscapeRoomValidation(IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync, LanguagesHelper languagesHelper)
        {
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;

            RuleFor(p => p.NombreEscapeRoom).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(this._languagesHelper.EscapeNomNullEmpty)
                .NotNull().WithMessage(this._languagesHelper.EscapeNomNullEmpty)
                .Length(3, 50).WithMessage(this._languagesHelper.EscapeNomLength)
                .Matches(@"^[ À-ÿA-Za-z0-9/_-]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z, 0-9, /_-")
                .MustAsync(async (model, nombreEscape, cancellation) =>
                {
                    return await IsExistNombreEscapeAsync(nombreEscape, model.Id, cancellation);
                }).WithMessage(this._languagesHelper.EscapeRoomExiste);


            RuleFor(f => f.FechaInicioJuego)
                .NotEmpty().WithMessage("Digite la 'Fecha Inicial'.")
                .GreaterThanOrEqualTo(x => new DateTime(2000, 1, 1, 0, 0, 0)).WithMessage("La 'Fecha Inicial'del juego  debe ser mayor al año 2000")
                //.LessThanOrEqualTo(x => DateTime.Now).WithMessage("La 'Fecha Inicial' del juego debe ser menor a la fecha actual")
                .LessThan(x => x.FechaFinJuego).WithMessage("La 'Fecha Inicial' del juego debe ser menor a la fecha final.");

            RuleFor(f => f.FechaFinJuego)
                .NotEmpty().WithMessage("Digite la 'Fecha Final'.")
                .GreaterThanOrEqualTo(x => new DateTime(2000, 1, 1, 0, 0, 0)).WithMessage("La 'Fecha Final' del juego debe ser mayor al año 2000")
                //.LessThanOrEqualTo(x => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)).WithMessage("La 'Fecha final' del juego debe ser menor a la fecha actual")
                .GreaterThan(x => x.FechaInicioJuego).WithMessage("La 'Fecha Final' del juego debe ser mayor a la fecha inicial.");

            RuleFor(p => p.TipoEscape).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Seleccione el 'Tipo' de escape room.")
                .Must(IsExistTipoEscape).WithMessage("El 'Tipo' de escape room no existe.");

            RuleFor(p => p.Organizador).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Nombre' del organizador.")
                .NotNull().WithMessage("Digite el 'Nombre' del organizador.")
                .Length(3, 30).WithMessage("El 'Nombre' del organizador debe tener entre 3 a 30 caracteres.")
                .Matches(@"^[ À-ÿA-Za-z]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} A-Z, a-z");


            RuleFor(p => p.CelularOrganizador).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Celular' del organizador.")
                .NotNull().WithMessage("Digite el 'Celular' del organizador.")
                .Length(10, 15).WithMessage("El 'Celular' del organizador debe tener entre 10 a 15 caracteres.")
                .Matches(@"^[ 0-9]+$").WithMessage($"{this._languagesHelper.CaracterInvalid} 0-9");


            RuleFor(p => p.TiempoLimiteParticipantes).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Tiempo' limite general para la duracion del escape en un dia.")
                .NotNull().WithMessage("Digite el 'Tiempo' limite general para la duracion del escape en un dia.")
                .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9](:[0-5][0-9])?$").WithMessage($"{this._languagesHelper.CaracterInvalid} 0-9, :");

            RuleFor(p => p.TiempoLimiteParticipantes).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Digite el 'Tiempo' limite para los participantes del escape room.")
                .NotNull().WithMessage("Digite el 'Tiempo' limite para los participantes del escape room.")
                .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9](:[0-5][0-9])?$").WithMessage($"{this._languagesHelper.CaracterInvalid} 0-9, :");

        }

        private async Task<bool> IsExistNombreEscapeAsync(string NombreEscape, int id, CancellationToken cancellationToken)
        {
            return await _escapeRoomsRepositoryAsync.IsExistAttributeAsync(e => !e.NombreEscapeRoom.ToLower().Equals(NombreEscape.ToLower()), x => x.Id != id);
        }

        private bool IsExistTipoEscape(int tipoEscape)
        {
            return Enum.IsDefined(typeof(TiposEscapes), tipoEscape);
        }
    }
}
