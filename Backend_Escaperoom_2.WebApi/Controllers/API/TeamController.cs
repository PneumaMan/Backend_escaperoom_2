using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador)]
    public class TeamController : BaseApiController
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public TeamController(ILogger<TeamController> logger, IMapper mapper, LanguagesHelper languagesHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los equipos con su debida paginacion
        /// </summary>        
        /// <param name="filter">Este paremetro es para filtar y mostrar los datos por sus opciones de filtrado</param>
        /// <response code="200">OK. Equipos para un escaperoom devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("pagination")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<TeamResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPagination([FromQuery] GetAllTeamParameter filter)
        {
            _logger.LogInformation("GET ALL Teams Pagination");
            return Ok(await Mediator.Send(_mapper.Map<GetAllTeamsPaginationRequest>(filter)));
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los equipos por su escape room sin paginacion
        /// </summary>
        /// <param name="idEscapeRoom">Este paremetro es para filtar y mostrar los datos por su escaperoom</param>
        /// <response code="200">OK. Listado de equipos devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("{idescaperoom}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<TeamResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(string idEscapeRoom)
        {
            _logger.LogInformation("GET ALL Teams");
            return Ok(await Mediator.Send(new GetAllTeamsRequest() { EscapeRoomId = idEscapeRoom }));
        }

        // GET api/<controller>/5
        /// <summary>
        /// Obtener el equipo por su id
        /// </summary>
        /// <param name="id">Este paremetro es buscar un espaque especifico por su id</param>
        /// <response code="200">OK. Equipos devueltos.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<TeamResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"GET Team id = {id}");
            return Ok(await Mediator.Send(new GetTeamByIdRequest { Id = id }));
        }

        // POST api/<controller>
        /// <summary>
        /// Permite crear un equipo en el sistema
        /// </summary>            
        /// <response code="200">OK. Equipo creado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPost]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateTeamResquest command)
        {
            _logger.LogInformation("POST Team");
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Permite actualizar un Team ya existente
        /// </summary>
        /// <param name="id">Parmetro que viene por defecto en la url</param>
        /// <param name="command">El objeto a actualizar</param>
        /// <response code="200">OK. Team actualizado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPut("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UpdateTeamResquest command)
        {
            _logger.LogInformation("PUT Team");
            if (id != command.Id)
            {
                var errors = new List<ValidationFailureResponse>()
                {
                    new ValidationFailureResponse("Id", "El 'Equipo' no existe.")
                };

                throw new ValidationException(errors);
            }

            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Permite eliminar un equipo ya existente
        /// </summary>
        /// <param name="id">Parmetro que viene por defecto en la url</param>
        /// <response code="200">OK. Equipo eliminado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpDelete("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            this._logger.LogWarning("DEL Team");
            return Ok(await Mediator.Send(new DeleteTeamRequest { Id = id }));
        }
    }
}
