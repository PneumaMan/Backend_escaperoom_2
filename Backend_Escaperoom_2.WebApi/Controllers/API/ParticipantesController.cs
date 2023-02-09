using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Queries;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipantesController : BaseApiController
    {
        private readonly ILogger<ParticipantesController> _logger;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public ParticipantesController(ILogger<ParticipantesController> logger, IMapper mapper, LanguagesHelper languagesHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los participantes con su debida paginacion por su escape room
        /// </summary>
        /// <param name="filter">Este paremetro es para filtar y mostrar los datos por sus opciones de filtrado</param>
        /// <response code="200">OK. Listado participantes devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("pagination")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ParticipanteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPagination([FromQuery] GetAllParticipantesParameter filter)
        {
            this._logger.LogInformation("GET ALL Participantes Pagination");
            return Ok(await Mediator.Send(this._mapper.Map<GetAllParticipantesPaginationRequest>(filter)));
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los participantes sin paginacion
        /// </summary>
        /// <param name="idEscapeRoom">Traer los datos por el id del escape room</param>
        /// <response code="200">OK. Listado retos devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<ParticipanteResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] string idEscapeRoom)
        {
            this._logger.LogInformation("GET ALL Participantes");
            return Ok(await Mediator.Send(new GetAllParticipantesRequest { EscapeRoomId = idEscapeRoom }));
        }

        // GET api/<controller>/5
        /// <summary>
        /// Obtener el participante por su id
        /// </summary>
        /// <param name="id">Este paremetro es buscar un espaque especifico por su id</param>
        /// <response code="200">OK. Participante devuelto.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<ParticipanteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            this._logger.LogInformation($"GET Participante id = {id}");
            return Ok(await Mediator.Send(new GetParticipanteByIdRequest { Id = id }));
        }

        // POST api/<controller>
        /// <summary>
        /// Permite crear un participante en el sistema para un escape room
        /// </summary>            
        /// <response code="200">OK. Participante creado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPost]
        [Authorize(Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador)]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateParticipanteResquest command)
        {
            this._logger.LogInformation("POST Participante");
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Permite actualizar un participante ya existente
        /// </summary>
        /// <param name="id">Parmetro que viene por defecto en la url</param>
        /// <param name="command">El objeto a actualizar</param>
        /// <response code="200">OK. Participante actualizado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPut("{id}")]
        [Authorize(Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador)]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UpdateParticipanteResquest command)
        {
            this._logger.LogInformation("PUT Participante");
            if (id != command.Id)
            {
                var errors = new List<ValidationFailureResponse>()
                {
                    new ValidationFailureResponse("Id", this._languagesHelper.RetoNoExiste)
                };

                throw new ValidationException(errors, this._languagesHelper.ErrorValidation);
            }

            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Permite eliminar un participante ya existente
        /// </summary>
        /// <param name="id">Parmetro que viene por defecto en la url</param>
        /// <response code="200">OK. Participante eliminado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpDelete("{id}")]
        [Authorize(Roles = RolesAuthorize.Desarrollador + "," + RolesAuthorize.Administrador)]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            this._logger.LogWarning("DELETE Participante");
            return Ok(await Mediator.Send(new DeleteParticipanteRequest { Id = id }));
        }
    }
}
