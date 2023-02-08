using Backend_Escaperoom_2.Application.Assets.Resources.Languages;
using Backend_Escaperoom_2.Application.Extension;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Backend_Escaperoom_2.Application.Helpers
{
    public class LanguagesHelper
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly CultureInfo _culture;
        private readonly LanguageSettings _languageSettings;

        public LanguagesHelper(IHttpContextAccessor httpcontextAccessor, IOptions<LanguageSettings> languageSettings)
        {
            _languageSettings = languageSettings.Value;
            _httpcontextAccessor = httpcontextAccessor;

            _culture = this._httpcontextAccessor.HttpContext.Request.Headers.GetCultureInfo(this._languageSettings.CurrentUICulture);
        }

        /*Global*/
        public string RazonSocial => LocalizationUtils<Resource>.GetValue(nameof(RazonSocial), this._culture);
        public string Meses => LocalizationUtils<Resource>.GetValue(nameof(Meses), this._culture);
        public string Años => LocalizationUtils<Resource>.GetValue(nameof(Años), this._culture);
        public string Hola => LocalizationUtils<Resource>.GetValue(nameof(Hola), this._culture);
        public string CaracterInvalid => LocalizationUtils<Resource>.GetValue(nameof(CaracterInvalid), this._culture);
        public string ErrorValidation => LocalizationUtils<Resource>.GetValue(nameof(ErrorValidation), this._culture);
        public string ErrorDispositivo => LocalizationUtils<Resource>.GetValue(nameof(ErrorDispositivo), this._culture);
        public string TokenOnForbidden => LocalizationUtils<Resource>.GetValue(nameof(TokenOnForbidden), this._culture);
        public string TokenUnauthorized => LocalizationUtils<Resource>.GetValue(nameof(TokenUnauthorized), this._culture);
        public string EmailNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(EmailNullEmpty), this._culture);
        public string EmailIncorrecto => LocalizationUtils<Resource>.GetValue(nameof(EmailIncorrecto), this._culture);
        public string EmailUse => LocalizationUtils<Resource>.GetValue(nameof(EmailUse), this._culture);
        public string PassNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(PassNullEmpty), this._culture);
        public string PassLenght => LocalizationUtils<Resource>.GetValue(nameof(PassLenght), this._culture);
        public string PassNoEquals => LocalizationUtils<Resource>.GetValue(nameof(PassNoEquals), this._culture);
        public string ConfirmPassNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(ConfirmPassNullEmpty), this._culture);
        public string ConfirmPassLenght => LocalizationUtils<Resource>.GetValue(nameof(ConfirmPassLenght), this._culture);
        public string NombreNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(NombreNullEmpty), this._culture);
        public string NombresLenght => LocalizationUtils<Resource>.GetValue(nameof(NombresLenght), this._culture);
        public string ApellidosNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(ApellidosNullEmpty), this._culture);
        public string ApellidosLenght => LocalizationUtils<Resource>.GetValue(nameof(ApellidosLenght), this._culture);
        public string FechaNaciNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(FechaNaciNullEmpty), this._culture);
        public string FechaNaciMayor => LocalizationUtils<Resource>.GetValue(nameof(FechaNaciMayor), this._culture);
        public string FechaNaciMenor => LocalizationUtils<Resource>.GetValue(nameof(FechaNaciMenor), this._culture);
        public string GeneroNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(GeneroNullEmpty), this._culture);
        public string TelefonoIncorrect => LocalizationUtils<Resource>.GetValue(nameof(TelefonoIncorrect), this._culture);
        public string CelularNull => LocalizationUtils<Resource>.GetValue(nameof(CelularNull), this._culture);
        public string CelularIncorrect => LocalizationUtils<Resource>.GetValue(nameof(CelularIncorrect), this._culture);
        public string MunicipioNull => LocalizationUtils<Resource>.GetValue(nameof(MunicipioNull), this._culture);
        public string DispositivoNull => LocalizationUtils<Resource>.GetValue(nameof(DispositivoNull), this._culture);
        public string TokenNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(TokenNullEmpty), this._culture);
        public string Home => LocalizationUtils<Resource>.GetValue(nameof(Home), this._culture);
        public string Politica => LocalizationUtils<Resource>.GetValue(nameof(Politica), this._culture);
        public string ControlGanadero => LocalizationUtils<Resource>.GetValue(nameof(ControlGanadero), this._culture);
        public string Login => LocalizationUtils<Resource>.GetValue(nameof(Login), this._culture);
        public string Logout => LocalizationUtils<Resource>.GetValue(nameof(Logout), this._culture);
        public string IndicadorPeso => LocalizationUtils<Resource>.GetValue(nameof(IndicadorPeso), this._culture);
        public string Email => LocalizationUtils<Resource>.GetValue(nameof(Email), this._culture);
        public string Password => LocalizationUtils<Resource>.GetValue(nameof(Password), this._culture);
        public string Recuerdame => LocalizationUtils<Resource>.GetValue(nameof(Recuerdame), this._culture);
        public string ForgotPassword => LocalizationUtils<Resource>.GetValue(nameof(ForgotPassword), this._culture);
        public string NoAccessLogin => LocalizationUtils<Resource>.GetValue(nameof(NoAccessLogin), this._culture);
        public string SeHaActualizado => LocalizationUtils<Resource>.GetValue(nameof(SeHaActualizado), this._culture);
        public string SeHaEliminado => LocalizationUtils<Resource>.GetValue(nameof(SeHaEliminado), this._culture);
        public string SeHaGuardado => LocalizationUtils<Resource>.GetValue(nameof(SeHaGuardado), this._culture);


        /*Titles*/
        public string TitleConfirmEmail => LocalizationUtils<Resource>.GetValue(nameof(TitleConfirmEmail), this._culture);
        public string TitleResetPassword => LocalizationUtils<Resource>.GetValue(nameof(TitleResetPassword), this._culture);
        public string TitleEmail => LocalizationUtils<Resource>.GetValue(nameof(TitleEmail), this._culture);
        public string TitleNewPassword => LocalizationUtils<Resource>.GetValue(nameof(TitleNewPassword), this._culture);
        public string TitleNewPassConfirm => LocalizationUtils<Resource>.GetValue(nameof(TitleNewPassConfirm), this._culture);


        /*Login Validation*/
        public string LoginEmailNoExist => LocalizationUtils<Resource>.GetValue(nameof(LoginEmailNoExist), this._culture);
        public string LoginEmailConfim => LocalizationUtils<Resource>.GetValue(nameof(LoginEmailConfim), this._culture);
        public string LoginEmailConfim2 => LocalizationUtils<Resource>.GetValue(nameof(LoginEmailConfim2), this._culture);
        public string LoginPassIncorrect => LocalizationUtils<Resource>.GetValue(nameof(LoginPassIncorrect), this._culture);
        public string LoginLicenciaNoUser => LocalizationUtils<Resource>.GetValue(nameof(LoginLicenciaNoUser), this._culture);
        public string LoginDispositivoNoUser => LocalizationUtils<Resource>.GetValue(nameof(LoginDispositivoNoUser), this._culture);
        public string PassPoliticaSeguridad => LocalizationUtils<Resource>.GetValue(nameof(PassPoliticaSeguridad), this._culture);



        /*Changed password*/
        public string ChangedOldPassNull => LocalizationUtils<Resource>.GetValue(nameof(ChangedOldPassNull), this._culture);
        public string ChangedOldPassLenght => LocalizationUtils<Resource>.GetValue(nameof(ChangedOldPassLenght), this._culture);
        public string ChangedNewPassNull => LocalizationUtils<Resource>.GetValue(nameof(ChangedNewPassNull), this._culture);
        public string ChangedNewPassLenght => LocalizationUtils<Resource>.GetValue(nameof(ChangedNewPassLenght), this._culture);
        public string ChangedNewPassEquals => LocalizationUtils<Resource>.GetValue(nameof(ChangedNewPassEquals), this._culture);
        public string ChangedConfirmNewPassNull => LocalizationUtils<Resource>.GetValue(nameof(ChangedConfirmNewPassNull), this._culture);
        public string ChangedConfirmNewPassLength => LocalizationUtils<Resource>.GetValue(nameof(ChangedConfirmNewPassLength), this._culture);


        /*Usuario*/
        public string UsuarioNoExiste => LocalizationUtils<Resource>.GetValue(nameof(UsuarioNoExiste), this._culture);
        public string UserCreated => LocalizationUtils<Resource>.GetValue(nameof(UserCreated), this._culture);
        public string LoginMobileReady => LocalizationUtils<Resource>.GetValue(nameof(LoginMobileReady), this._culture);
        public string LoginReady => LocalizationUtils<Resource>.GetValue(nameof(LoginReady), this._culture);
        public string UserNoActivo => LocalizationUtils<Resource>.GetValue(nameof(UserNoActivo), this._culture);

        /*EscapeRoom*/
        public string EscapeNomNullEmpty => LocalizationUtils<Resource>.GetValue(nameof(EscapeNomNullEmpty), this._culture);
        public string EscapeNomLength => LocalizationUtils<Resource>.GetValue(nameof(EscapeNomLength), this._culture);
        public string EscapeRoomExiste => LocalizationUtils<Resource>.GetValue(nameof(EscapeRoomExiste), this._culture);
        public string EscapeRoomNoExiste => LocalizationUtils<Resource>.GetValue(nameof(EscapeRoomNoExiste), this._culture);


        /*Reto*/
        public string RetoNoExiste => LocalizationUtils<Resource>.GetValue(nameof(RetoNoExiste), this._culture);
        public string RetoExiste => LocalizationUtils<Resource>.GetValue(nameof(RetoExiste), this._culture);



        /*Participante*/
        public string ParticipanteNoExiste => LocalizationUtils<Resource>.GetValue(nameof(ParticipanteNoExiste), this._culture);
        public string EstadoParticipanteNoExiste => LocalizationUtils<Resource>.GetValue(nameof(EstadoParticipanteNoExiste), this._culture);
        public string ParticipanteExiste => LocalizationUtils<Resource>.GetValue(nameof(ParticipanteExiste), this._culture);



        /*Respuesta*/
        public string RespuestaNoExiste => LocalizationUtils<Resource>.GetValue(nameof(RespuestaNoExiste), this._culture);


        /*Tipo Reto*/
        public string TipoRetoNoExiste => LocalizationUtils<Resource>.GetValue(nameof(TipoRetoNoExiste), this._culture);


        /*Tipo Pregunta*/
        public string TipoPreguntaNoExiste => LocalizationUtils<Resource>.GetValue(nameof(TipoPreguntaNoExiste), this._culture);


        /*Tipo Usuario*/
        public string TipoUsuarioNull => LocalizationUtils<Resource>.GetValue(nameof(TipoUsuarioNull), this._culture);


        /*Changed Password*/
        public string ChangedPassword => LocalizationUtils<Resource>.GetValue(nameof(ChangedPassword), this._culture);


        /*HTML*/
        public string RestorePassword => LocalizationUtils<Resource>.GetValue(nameof(RestorePassword), this._culture);
    }
}
