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
    [Table("Respuestas")]
    public class RespuestaRetos : AuditableBaseEntity
    {
        [Key, Column("id_respuesta")]
        public int Id { get; set; }

        [Column("respuesta_reto"), MaxLength(200), Required]
        public string RespuestaReto { get; set; }

        [Column("correcta")]
        public bool Correcta { get; set; }

        [Column("posicion_llave")]
        public int? PosicionLlave { get; set; }

        [Column("llave"), MaxLength(100)]
        public string Llave { get; set; }


        [Column("palabra_reto_retorno"), MaxLength(200)]
        public string PalabraRetoRetorno { get; set; }

        [Column("id_reto"), ForeignKey("Retos")]
        public int RetoId { get; set; }
        public Reto Reto { get; set; }

        [Column("id_next_estacion"), ForeignKey("Estacion")]
        public int? NextEstacionId { get; set; }
        public Estacion NextEstacion { get; set; }


        public ICollection<ParticipantesRespuestas> ParticipantesRespuestas { get; set; }
    }
}
