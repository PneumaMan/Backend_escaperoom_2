using Backend_Escaperoom_2.Application.DTOs.WebApi.Email;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Backend_Escaperoom_2.Application.DTOs.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Usuario> GetUserById(string id);

        Task<Usuario> GetUserByEmail(string email);

        Task UpdateUser(Usuario user);

        Task DeleteUser(Usuario user);

        Task LogoutAsync();

        Task<List<string>> GetRolesByUserAsync(Usuario user);

        Task<SignInResult> ValidatePasswordAsync(string username, string Password, bool? rememberMe = null);

        Task<Response<AuthenticationResponse>> AuthenticateAsync(Usuario user, string ipAddress);

        Task<Response<string>> CreateAsync(Usuario user, string password, List<string> roles);

        Task<Response<string>> ChangedPassword(Usuario user, string oldPassword, string newPassword);
    }
}
