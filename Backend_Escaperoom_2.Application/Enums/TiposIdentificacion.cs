using System.ComponentModel;

namespace Backend_Escaperoom_2.Application.Enums
{
    public enum TiposIdentificacion
    {
        /// <summary>
        /// NIT
        /// </summary>
        [Description("NIT")]
        NIT = 1,

        /// <summary>
        /// Cédula de ciudadania
        /// </summary>
        [Description("Cédula de ciudadania")]
        CC = 2,

        /// <summary>
        /// Cedula de extranjeria
        /// </summary>
        [Description("Cédula de extranjeria")]
        CE = 3,

        /// <summary>
        /// Registro civil
        /// </summary>
        [Description("Registro civil")]
        RC = 4,

        /// <summary>
        /// Tarjeta de identidad
        /// </summary>
        [Description("Tarjeta de identidad")]
        TI = 5,

        /// <summary>
        /// Pasaporte
        /// </summary>
        [Description("Pasaporte")]
        PA = 6,

        /// <summary>
        /// Adulto sin identificar
        /// </summary>
        [Description("Adulto sin identificar")]
        AS = 7,

        /// <summary>
        /// Adulto sin identificar
        /// </summary>
        [Description("Tarjeta Profesional")]
        TP = 8,

        /// Adulto sin identificar
        /// </summary>
        [Description("Código Estudiantil Universitario")]
        CEU = 9,
    }
}
