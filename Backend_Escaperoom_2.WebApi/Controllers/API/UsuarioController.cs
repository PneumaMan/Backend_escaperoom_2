using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.WebApi.Controllers.API.BaseController;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.WebApi.Controllers.API
{
    [Route("api/cuenta")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : BaseApiController
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;

        public UsuarioController(ILogger<UsuarioController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // POST CambiarContrasena
        /// <summary>
        /// Permite cambiar la contraseña de un usuario
        /// </summary>
        /// <response code="200">OK. Devuelve el si ejecuto la operacion.</response>
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response> 
        /// <response code="401">Unauthorized. Debe iniciar sesión, para acceder a este recurso.</response>  
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>  
        [HttpPost("Changed-Password")]
        [Authorize(Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador + "," + RolesAuthorize.Participante)]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ValidationFailureResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangedPassword(ChangedPasswordRequest command)
        {
            this._logger.LogInformation("POST ChangedPassword");
            return Ok(await Mediator.Send(command));
        }
    }
}
