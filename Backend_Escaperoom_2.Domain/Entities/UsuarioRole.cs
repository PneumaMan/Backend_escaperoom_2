using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Usuario_Roles")]
    public class UsuarioRole : IdentityUserRole<string>
    {
        public Usuario Usuario { get; set; }

        public Role Role { get; set; }
    }
}
