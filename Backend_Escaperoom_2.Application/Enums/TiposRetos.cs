using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum TiposRetos
    {
        [Description("Reto Normal")]
        RetoNormal = 1,

        [Description("Reto Llave")]
        RetoLlave = 2,

        [Description("Reto Especial")]
        RetoEspecial = 3,

        [Description("Reto Bonificación")]
        RetoBonificacion = 4,

        [Description("Reto Normal con retorno de plabra")]
        RetoNormalRespuesta = 5,
    }
}
