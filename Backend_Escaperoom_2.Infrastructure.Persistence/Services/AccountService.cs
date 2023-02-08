using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using Backend_Escaperoom_2.Domain.Settings;
using Backend_Escaperoom_2.Infrastructure.Persistence.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly IUrlServices _urlServices;
        private readonly IDataProtector _protector;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Role> _roleInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly URLSettings _urlSettings;
        private readonly LanguagesHelper _languagesHelper;

        public AccountService(UserManager<Usuario> userManager, LanguagesHelper languagesHelper,
            IOptions<JWTSettings> jwtSettings, IDateTimeService dateTimeService, IDataProtectionProvider protectionProvider,
            SignInManager<Usuario> signInManager, RoleManager<Role> roleInManager,
            IMapper mapper, IOptions<URLSettings> urlSettings, IUrlServices urlServices)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _urlSettings = urlSettings.Value;
            _urlServices = urlServices;
            _signInManager = signInManager;
            _roleInManager = roleInManager;
            _languagesHelper = languagesHelper;
            _mapper = mapper;
            _protector = protectionProvider.CreateProtector(nameof(IAccountService));
        }

        public async Task<Usuario> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task UpdateUser(Usuario user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(Usuario user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<string>> GetRolesByUserAsync(Usuario user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<SignInResult> ValidatePasswordAsync(string username, string Password, bool? rememberMe = null)
        {
            if (rememberMe != null)
            {
                return await _signInManager.PasswordSignInAsync(username, Password, (bool)rememberMe, false);
            }
            else
            {
                return await _signInManager.PasswordSignInAsync(username, Password, false, lockoutOnFailure: false);
            }
        }


        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(Usuario user, string ipAddress)
        {

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = this._protector.Protect(user.Id),
                Email = user.Email,
                IsActived = user.EstadoUsuario,
                ChangedPassword = user.ChangedPassword,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpireToken = jwtSecurityToken.ValidTo,
                Roles = rolesList.ToList(),
                RefreshToken = this.GenerateRefreshToken(ipAddress).Token
            };

            user.LastSignIn = this._dateTimeService.Now;
            await _userManager.UpdateAsync(user);

            return new Response<AuthenticationResponse>(response, "Ha iniciado sesión correctamente.");
        }

        public async Task<Response<string>> CreateAsync(Usuario user, string password, List<string> roles)
        {
            foreach (var rol in roles)
            {
                var res = await _roleInManager.FindByNameAsync(rol);
                if (res == null)
                {
                    throw new ApiException($"El Rol o el tipo de usuario no existe. ({rol})");
                }
            }

            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                foreach (var rol in roles)
                {
                    await _userManager.AddToRoleAsync(user, rol);
                }

                //await SendVerificationEmail(user);

                return new Response<string>(user.Id, "El usuario ha sido creado exitosamente.");
            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
        }

        public async Task<Response<string>> ChangedPassword(Usuario user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, this._languagesHelper.ChangedPassword);
            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
        }

        //TOKENS
        private async Task<JwtSecurityToken> GenerateJWToken(Usuario user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this._jwtSettings.Issuer,
                audience: this._jwtSettings.Audience,
                claims: claims,
                expires: this._dateTimeService.Now.AddHours(this._jwtSettings.DurationInHours),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = this._dateTimeService.Now.AddDays(this._jwtSettings.RefreshTokenDurationInDays),
                Created = this._dateTimeService.Now,
                CreatedByIp = ipAddress
            };
        }

    }

}
