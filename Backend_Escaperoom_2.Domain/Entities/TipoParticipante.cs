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
    [Table("Tipo_Participante")]
    public class TipoParticipante : AuditableBaseEntity
    {
        [Key, Column("id_tipo_participante")]
        public int Id { get; set; }

        [Column("nombre_tipo"), MaxLength(100), Required]
        public string NombreTipo { get; set; }

        [Column("descripcion"), MaxLength(200), Required]
        public string Descripcion { get; set; }

        [Column("estado")]
        public bool Estado { get; set; }

        [Column("id_escape_room"), ForeignKey("Escape_Room")]
        public int EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }

        public ICollection<Participante> Participantes { get; set; }

        public ICollection<Reto> Retos { get; set; }
    }
}
