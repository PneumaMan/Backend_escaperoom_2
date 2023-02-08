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
    [Table("Participantes_Respuestas")]
    public class ParticipantesRespuestas : AuditableBaseEntity
    {
        [Column("tiempo_respuesta")]
        public TimeSpan TiempoRespuesta { get; set; }

        [Column("fecha_respuesta")]
        public DateTime FechaRespuesta { get; set; }


        [Column("id_participante"), ForeignKey("Participantes")]
        public int ParticipanteId { get; set; }

        public Participante Participante { get; set; }


        [Column("id_respuesta"), ForeignKey("Respuestas")]
        public int RespuestaId { get; set; }

        public RespuestaRetos Respuesta { get; set; }

        [Column("id_reto"), ForeignKey("Retos")]
        public int RetoId { get; set; }

        public Reto Reto { get; set; }
    }
}
