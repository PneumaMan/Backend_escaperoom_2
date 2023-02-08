using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum TiposUsuarios
    {
        [Description("Desarrollador")]
        Desarrollador = 1,

        [Description("Administrador")]
        Administrador = 2,

        [Description("Participante")]
        Participante = 3
    }

    public static class RolesAuthorize
    {
        public const string Desarrollador = "Desarrollador";
        public const string Administrador = "Administrador";
        public const string Participante = "Participante";
    }
}


