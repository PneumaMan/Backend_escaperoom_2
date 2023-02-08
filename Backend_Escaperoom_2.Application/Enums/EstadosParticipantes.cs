using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum EstadosParticipantes
    {
        [Description("Participante registrado")]
        Registrado = 1,

        [Description("Participante en juego")]
        EnJuego = 2,

        [Description("Participante termino el juego")]
        Terminado = 3,

        [Description("Participante deshabilitado")]
        Deshabilitado = 4,

        [Description("Participante repitiendo juego")]
        EnJuegoRepitiendo = 5,
    }
}
