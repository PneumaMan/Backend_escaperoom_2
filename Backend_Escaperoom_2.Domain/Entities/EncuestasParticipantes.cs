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
    [Table("Encuestas_Participantes")]
    public class EncuestasParticipantes : AuditableBaseEntity
    {
        [Column("respuesta_abierta")]
        public string RespuestaAbierta { get; set; }

        [Column("fecha_respuesta")]
        public DateTime FechaRespuesta { get; set; }


        [Column("id_participante"), ForeignKey("Participantes")]
        public int ParticipanteId { get; set; }

        public Participante Participante { get; set; }

        [Column("id_pregunta_encuesta"), ForeignKey("Preguntas_Encuestas")]
        public int PreguntaEncuestasId { get; set; }

        public PreguntaEncuestas PreguntaEncuesta { get; set; }


        [Column("id_respuesta_encuesta"), ForeignKey("Respuestas_Encuestas")]
        public int? RespuestasEncuestasId { get; set; }

        public RespuestaEncuestas RespuestasEncuesta { get; set; }
    }
}
