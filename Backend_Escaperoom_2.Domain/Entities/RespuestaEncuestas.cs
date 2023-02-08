using Backend_Escaperoom_2.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Respuestas_Encuestas")]
    public class RespuestaEncuestas : AuditableBaseEntity
    {
        [Key, Column("id_respuesta_encuesta")]
        public int Id { get; set; }

        [Column("respuesta"), MaxLength(150)]
        public string Respuesta { get; set; }


        [Column("id_pregunta_encuesta"), ForeignKey("Preguntas_Encuestas")]
        public int PreguntaEncuestasId { get; set; }
        public PreguntaEncuestas PreguntaEncuestas { get; set; }

        public ICollection<EncuestasParticipantes> EncuestasParticipantes { get; set; }
    }
}
