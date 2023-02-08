using Microsoft.AspNetCore.Http;
using Backend_Escaperoom_2.Application.Interfaces;
using System.Security.Claims;

namespace Backend_Escaperoom_2.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId { get; }

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }
    }
}
