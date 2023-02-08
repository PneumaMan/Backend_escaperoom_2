using Backend_Escaperoom_2.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Escape_Room")]
    public class EscapeRoom : AuditableBaseEntity
    {
        [Key, Column("id_escape_room")]
        public int Id { get; set; }

        [Column("nombre_escape_room"), MaxLength(100), Required]
        public string NombreEscapeRoom { get; set; }

        [Column("estado")]
        public int Estado { get; set; }

        [Column("tipo_escape")]
        public int TipoEscape { get; set; }

        [Column("fecha_inicio_juego")]
        public DateTime FechaInicioJuego { get; set; }

        [Column("fecha_fin_juego")]
        public DateTime FechaFinJuego { get; set; }

        [Column("organizador"), MaxLength(30), Required]
        public string Organizador { get; set; }

        [Column("celular_organizador"), MaxLength(15), Required]
        public string CelularOrganizador { get; set; }

        [Column("tiempo_limite_general")]
        public TimeSpan TiempoLimiteGeneral { get; set; }

        [Column("tiempo_limite_participantes")]
        public TimeSpan TiempoLimiteParticipantes { get; set; }

        public ICollection<Participante> Participantes { get; set; }

        public ICollection<Estacion> Estaciones { get; set; }

        public ICollection<TipoParticipante> TipoParticipantes { get; set; }

        public ICollection<Team> Equipos { get; set; }
        public ICollection<Encuestas> Encuestas { get; set; }
    }
}
