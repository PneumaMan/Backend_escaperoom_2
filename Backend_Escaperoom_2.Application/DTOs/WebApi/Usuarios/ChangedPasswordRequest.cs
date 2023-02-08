using MediatR;
using Backend_Escaperoom_2.Application.Wrappers;

namespace Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios
{
    public class ChangedPasswordRequest : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }

    }
}
