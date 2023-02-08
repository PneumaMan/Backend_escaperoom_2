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
    [Table("Participantes")]
    public class Participante : AuditableBaseEntity
    {
        [Key, Column("id_participante")]
        public int Id { get; set; }

        [Column("tipo_identificacion"), Required]
        public int TipoIdentificacion { get; set; }

        [Column("identificacion"), MaxLength(30), Required]
        public string Identificacion { get; set; }

        [Column("nombres"), MaxLength(100), Required]
        public string Nombres { get; set; }

        [Column("apellidos"), MaxLength(100), Required]
        public string Apellidos { get; set; }

        [Column("telefono")]
        public long? Telefono { get; set; }

        [Column("estado")]
        public int Estado { get; set; }

        [Column("time_score")]
        public TimeSpan? TimeScore { get; set; }


        [Column("id_escape_room"), ForeignKey("Escape_Room")]
        public int EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }


        [Column("id_tipo_participante"), ForeignKey("Tipo_Participante")]
        public int TipoParticipanteId { get; set; }
        public TipoParticipante TipoParticipante { get; set; }


        [Column("id_team"), ForeignKey("Team")]
        public int? TeamId { get; set; }
        public Team MyTeam { get; set; }


        public ICollection<ParticipantesRespuestas> ParticipantesRespuestas { get; set; }

        public ICollection<EncuestasParticipantes> EncuestasParticipantes { get; set; }
    }
}
