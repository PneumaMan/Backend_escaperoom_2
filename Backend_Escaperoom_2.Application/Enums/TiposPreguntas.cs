using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum TiposPreguntas
    {
        [Description("Selección Multiple (Unica)")]
        SeleccionMultipleUnica = 1,

        [Description("Selección Multiple (Varias)")]
        SeleccionMultiple = 2,

        [Description("Respuesta Abierta")]
        RespuestaAbierta = 3,

        [Description("Respuesta Si/No")]
        SiNo = 4,

        [Description("Lista desplegable")]
        ListaDesplegable = 5,
    }
}
