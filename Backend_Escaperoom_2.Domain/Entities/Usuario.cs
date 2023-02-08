using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Escaperoom_2.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario : IdentityUser
    {
        [Column("TipoLogueo"), Required]
        public int TipoLogueo { get; set; }

        [Column("EstadoUsuario"), Required]
        public bool EstadoUsuario { get; set; }

        [Column("ChangedPassword"), Required]
        public bool ChangedPassword { get; set; }

        [Column("Registered"), Required]
        public DateTime Registered { get; set; }

        [Column("LastSignin")]
        public DateTime? LastSignIn { get; set; }

        public ICollection<UsuarioRole> UsuariosRoles { get; set; }
    }
}
