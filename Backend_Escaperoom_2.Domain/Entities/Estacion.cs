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
    [Table("Estacion")]
    public class Estacion : AuditableBaseEntity
    {
        [Key, Column("id_estacion")]
        public int Id { get; set; }

        [Column("nombre_estacion"), MaxLength(100), Required]
        public string NombreEscapeRoom { get; set; }

        [Column("descripcion"), MaxLength(200), Required]
        public string Descripcion { get; set; }

        [Column("estado")]
        public bool Estado { get; set; }

        [Column("contexto_estacion")]
        public string ContextoEstacion { get; set; }

        [Column("path_multimedia")]
        public string PathMultimedia { get; set; }

        [Column("tipo_multimedia")]
        public int TipoMultimedia { get; set; }

        [Column("id_escape_room"), ForeignKey("Escape_Room")]
        public int EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }

        public ICollection<Reto> Retos { get; set; }
    }
}
