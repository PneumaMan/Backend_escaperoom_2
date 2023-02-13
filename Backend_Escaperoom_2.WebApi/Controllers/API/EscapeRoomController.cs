using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries;
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
        Roles = RolesAuthorize.Administrador)]
    public class EscapeRoomController : BaseApiController
    {
        private readonly ILogger<EscapeRoomController> _logger;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public EscapeRoomController(ILogger<EscapeRoomController> logger, IMapper mapper, LanguagesHelper languagesHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los escaperooms con su debida paginacion
        /// </summary>        
        /// <param name="filter">Este paremetro es para filtar y mostrar los datos por sus opciones de filtrado</param>
        /// <response code="200">OK. Listado escaperooms devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("pagination")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<EscapeRoomResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPagination([FromQuery] GetAllEscapeRoomParameter filter)
        {
            _logger.LogInformation("GET ALL EscapeRooms Pagination");
            return Ok(await Mediator.Send(_mapper.Map<GetAllEscapeRoomsPaginationRequest>(filter)));
        }

        // GET: api/<controller>
        /// <summary>
        /// Se obtiene un listado de los escaperooms para sin paginacion
        /// </summary>        
        /// <response code="200">OK. Listado escaperooms devueltos.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<IEnumerable<EscapeRoomResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GET ALL EscapeRooms");
            return Ok(await Mediator.Send(new GetAllEscapeRoomsRequest()));
        }

        // GET api/<controller>/5
        /// <summary>
        /// Obtener el escaperrom por su id
        /// </summary>
        /// <param name="id">Este paremetro es buscar un espaque especifico por su id</param>
        /// <response code="200">OK. Escaperoom devuelta</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpGet("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<EscapeRoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"GET EscapeRoom id = {id}");
            return Ok(await Mediator.Send(new GetEscapeRoomByIdRequest { Id = id }));
        }

        // POST api/<controller>
        /// <summary>
        /// Permite crear un escape room en el sistema
        /// </summary>            
        /// <response code="200">OK. Escaperoom creado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPost]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateEscapeRoomResquest command)
        {
            _logger.LogInformation("POST EscapeRoom");
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Permite actualizar un EscapeRoom ya existente
        /// </summary>
        /// <param name="id">Parmetro que viene por defecto en la url</param>
        /// <param name="command">El objeto a actualizar</param>
        /// <response code="200">OK. Escaperoom actualizado.</response>       
        /// <response code="400">BadRequest. Se han producido uno o más errores de validación.</response>   
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>    
        [HttpPut("{id}")]
        [Produces("application/json", "text/html")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UpdateEscapeRoomResquest command)
        {
            _logger.LogInformation("PUT EscapeRoom");
            if (id != command.Id)
            {
                var errors = new List<ValidationFailureResponse>()
                {
                    new ValidationFailureResponse("Id", _languagesHelper.EscapeRoomNoExiste)
                };

                throw new ValidationException(errors, _languagesHelper.ErrorValidation);
            }

            return Ok(await Mediator.Send(command));
        }
    }
}
