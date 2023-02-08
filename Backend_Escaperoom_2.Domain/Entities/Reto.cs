using Backend_Escaperoom_2.Domain.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Retos")]
    public class Reto : AuditableBaseEntity
    {
        [Key, Column("id_reto")]
        public int Id { get; set; }

        [Column("nombre_reto"), MaxLength(100), Required]
        public string NombreReto { get; set; }

        [Column("contexto_reto")]
        public string ContextoReto { get; set; }

        [Column("pregunta_reto"), MaxLength(250), Required]
        public string PreguntaReto { get; set; }

        [Column("tipo_pregunta")]
        public int TipoPregunta { get; set; }

        [Column("numero_reto")]
        public int NumeroReto { get; set; }

        [Column("obligatorio")]
        public bool Obligatorio { get; set; }

        [Column("tipo_reto")]
        public int TipoReto { get; set; }

        [Column("bonificacion")]
        public TimeSpan? Bonificacion { get; set; }

        [Column("numero_oportunidades")]
        public int NumeroOportunidades { get; set; }

        [Column("qr_color"), MaxLength(10)]
        public string QRColor { get; set; }

        [Column("qr_bg_color"), MaxLength(10)]
        public string QRBgColor { get; set; }

        [Column("path_multimedia")]
        public string PathMultimedia { get; set; }

        [Column("tipo_multimedia")]
        public int TipoMultimedia { get; set; }

        [Column("id_estacion"), ForeignKey("Estacion")]
        public int EstacionId { get; set; }
        public Estacion Estacion { get; set; }

        [Column("id_tipo_participante"), ForeignKey("Tipo_Participante")]
        public int TipoParticipanteId { get; set; }
        public TipoParticipante TipoParticipante { get; set; }


        [Column("id_reto_padre"), ForeignKey("Reto")]
        public int? RetoPadreId { get; set; }
        public Reto RetoPadre { get; set; }

        public ICollection<RespuestaRetos> Respuestas { get; set; }

        public ICollection<ParticipantesRespuestas> ParticipantesRespuestas { get; set; }
    }
}
