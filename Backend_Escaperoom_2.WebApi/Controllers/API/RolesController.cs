using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Roles;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.WebApi.Controllers.API.BaseController;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.WebApi.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : BaseApiController
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;

        public RolesController(ILogger<RolesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los roles del sistema sin paginacion
        /// </summary>        
        /// <response code="200">OK. Roles devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<RolesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Authorize(Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador)]
        public async Task<IActionResult> GetAll()
        {
            this._logger.LogInformation("GET ALL roles");
            return Ok(await Mediator.Send(new GetAllRolesRequest()));

        }
    }
}
