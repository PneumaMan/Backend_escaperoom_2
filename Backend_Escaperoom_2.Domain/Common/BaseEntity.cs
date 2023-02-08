using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend_Escaperoom_2.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
