using System.ComponentModel.DataAnnotations;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Web
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Digite el 'Correo electrónico'.")]
        [RegularExpression(@"^((?!^Email$)[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9]))?$", ErrorMessage = "El 'Correo electrónico' es incorrecto.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "La 'Contraseña' debe tener entre 8 a 20 caracteres.")]
        [MaxLength(20, ErrorMessage = "La 'Contraseña' debe tener entre 8 a 20 caracteres.")]
        [Required(ErrorMessage = "Digite la 'Contraseña'.")]
        //[RegularExpression(@"^((?!^Password$)(?=.{8,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).)+$", ErrorMessage = "La 'Contraseña' debe contener al menos una Mayúscula, una Minúscula, un simbolo y un número.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
