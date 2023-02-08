using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        [Column("create_by")]
        public string CreatedBy { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("last_modified_by")]
        public string LastModifiedBy { get; set; }

        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
    }
}
