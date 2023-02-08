using System;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios
{
    public class UsuarioResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool EstadoUsuario { get; set; }

        public DateTime SingUpDate { get; set; }

        public DateTime? LastSignIn { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
