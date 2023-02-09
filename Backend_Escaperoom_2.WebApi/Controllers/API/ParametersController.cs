using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Parameters;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.WebApi.Controllers.API.BaseController;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.WebApi.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(Roles = RolesAuthorize.Administrador + "," + RolesAuthorize.Participante)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParametersController : BaseApiController
    {
        private readonly ILogger<ParametersController> _logger;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public ParametersController(ILogger<ParametersController> logger, IMapper mapper, LanguagesHelper languagesHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        /// <summary>
        /// Se obtiene un listado de los tipos de retos del escape room
        /// </summary>            
        /// <response code="200">OK. Tipos de retos devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("tipos-retos")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTiposRetos()
        {
            this._logger.LogInformation("GET ALL TiposRetos");
            return Ok(await Mediator.Send(new GetAllTiposRetosRequest()));
        }

        /// <summary>
        /// Se obtiene un listado de los tipos de preguntas para los retos del escape room
        /// </summary>            
        /// <response code="200">OK. Tipos de preguntas devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("tipos-preguntas")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTiposPreguntas()
        {
            this._logger.LogInformation("GET ALL TiposPreguntas");
            return Ok(await Mediator.Send(new GetAllTiposPreguntasRequest()));
        }

        /// <summary>
        /// Se obtiene un listado de los estados de los participantes
        /// </summary>            
        /// <response code="200">OK. Estados de los participantes devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("estados-participantes")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEstadosParticipantes()
        {
            this._logger.LogInformation("GET ALL EstadosParticipantes");
            return Ok(await Mediator.Send(new GetAllEstadosParticipantesRequest()));
        }

        /// <summary>
        /// Se obtiene un listado de los estados de los escape rooms
        /// </summary>            
        /// <response code="200">OK. Estados de los escape rooms devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("estados-escapes")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEstadosEscapes()
        {
            this._logger.LogInformation("GET ALL EstadosEscapes");
            return Ok(await Mediator.Send(new GetAllEstadosEscapesRequest()));
        }

        /// <summary>
        /// Se obtiene un listado de los tipos de escapes rooms
        /// </summary>            
        /// <response code="200">OK. Tipos de los escape rooms devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("tipos-escapes")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTiposEscapes()
        {
            this._logger.LogInformation("GET ALL TiposEscapes");
            return Ok(await Mediator.Send(new GetAllTiposEscapesRequest()));
        }

        /// <summary>
        /// Se obtiene un listado de los tipos de identificacion para los participantes
        /// </summary>            
        /// <response code="200">OK. Tipos de identificaion devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("tipos-identificacion")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EnumResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTiposIdentificacion()
        {
            this._logger.LogInformation("GET ALL TiposIdentificacion");
            return Ok(await Mediator.Send(new GetAllTiposIdentificacionRequest()));
        }
    }
}
