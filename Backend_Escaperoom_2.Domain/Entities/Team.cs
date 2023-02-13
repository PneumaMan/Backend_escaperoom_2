using Backend_Escaperoom_2.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Team")]
    public class Team : AuditableBaseEntity
    {
        [Key, Column("id_team")]
        public int Id { get; set; }

        [Column("nombre_team"), MaxLength(50), Required]
        public string NombreTeam { get; set; }

        [Column("capacidad")]
        public int Capacidad { get; set; }

        [Column("time_score_grupal")]
        public TimeSpan? TimeScoreTeam { get; set; }

        [Column("id_escape_room"), ForeignKey("Escape_Room")]
        public int EscapeRoomId { get; set; }
        public EscapeRoom EscapeRoom { get; set; }

        public ICollection<Participante> Participantes { get; set; }
    }
}
