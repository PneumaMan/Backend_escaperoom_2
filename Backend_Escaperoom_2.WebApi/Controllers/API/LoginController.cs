using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.Autentificacion;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.WebApi.Controllers.API.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.WebApi.Controllers.API
{
    [Route("api")]
    public class LoginController : BaseApiController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(ILogger<LoginController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // POST: api/Login
        /// <summary>
        /// Autentificacion en el sistema
        /// </summary>            
        /// <response code="200">OK. Ha iniciado sesión correctamente.</response>        
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>    
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPost("Login")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest command)
        {
            this._logger.LogInformation("POST Login");
            command.IpAddress = this.GenerateIPAddress();
            return Ok(await Mediator.Send(command));
        }

        // POST: api/Logout
        /// <summary>
        /// Desloguearse correctamente del sistema
        /// </summary>            
        /// <response code="200">OK. Se ha deslogueado correctamente.</response>        
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>    
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPost("Logout")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeauthenticateAsync(LogoutRequest command)
        {
            this._logger.LogInformation("POST Logout");
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Metodo privado para obtener la ip
        /// </summary>
        /// <returns></returns>
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}