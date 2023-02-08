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
    [Table("Encuestas")]
    public class Encuestas : AuditableBaseEntity
    {
        [Key, Column("id_encuesta")]
        public int Id { get; set; }

        [Column("nombre_encuesta"), MaxLength(100), Required]
        public string NombreEncuesta { get; set; }

        [Column("descripcion"), MaxLength(200), Required]
        public string Descripcion { get; set; }

        [Column("estado")]
        public bool Estado { get; set; }

        [Column("id_escape_room"), ForeignKey("Escape_Room")]
        public int EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }

        public ICollection<PreguntaEncuestas> PreguntasEncuestas { get; set; }
    }
}
