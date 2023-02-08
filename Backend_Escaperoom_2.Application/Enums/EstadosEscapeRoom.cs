using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum EstadosEscapeRoom
    {
        [Description("Escape room activo")]
        Activo = 1,

        [Description("Escape room en juego")]
        EnJuego = 2,

        [Description("Escape room finalizado")]
        Finalizado = 3,

        [Description("Escape room inactivo")]
        Inactivo = 4
    }
}
