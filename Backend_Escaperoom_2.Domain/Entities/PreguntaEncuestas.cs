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
    [Table("Preguntas_Encuestas")]
    public class PreguntaEncuestas : AuditableBaseEntity
    {
        [Key, Column("id_pregunta_encuesta")]
        public int Id { get; set; }

        [Column("pregunta_encuesta"), MaxLength(150), Required]
        public string PreguntaEncuesta { get; set; }

        [Column("descripcion"), MaxLength(200), Required]
        public string Descripcion { get; set; }

        [Column("tipo_pregunta")]
        public int TipoPregunta { get; set; }

        [Column("numero_orden")]
        public int NumeroOrden { get; set; }

        [Column("path_multimedia")]
        public string PathMultimedia { get; set; }

        [Column("tipo_multimedia")]
        public int TipoMultimedia { get; set; }

        [Column("id_encuesta"), ForeignKey("Encuestas")]
        public int EncuestasId { get; set; }
        public Encuestas Encuesta { get; set; }

        public ICollection<RespuestaEncuestas> RespuestasEncuestas { get; set; }

        public ICollection<EncuestasParticipantes> EncuestasParticipantes { get; set; }
    }
}
