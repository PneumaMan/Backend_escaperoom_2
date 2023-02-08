using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Role")]
    public class Role : IdentityRole
    {
        public ICollection<UsuarioRole> UsuariosRoles { get; set; }
    }
}
