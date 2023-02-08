using AutoMapper;
using MediatR;
using Backend_Escaperoom_2.Application.DTOs.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.Enums;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend_Escaperoom_2.Application.Interfaces;

namespace Backend_Escaperoom_2.Application.Features.Usuarios.Commands
{
    public class CreateUsuarioCommand : IRequestHandler<CreateUsuarioRequest, Response<string>>
    {
        private readonly IAccountService _usuarioService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;

        public CreateUsuarioCommand(IAccountService usuarioService, IMapper mapper, IDateTimeService dateTimeService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<string>> Handle(CreateUsuarioRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Usuario>(request);

            user.UserName = request.Email;
            user.EstadoUsuario = true;
            user.Registered = this._dateTimeService.Now;
            user.TipoLogueo = (int)TiposLogueo.Correo;

            return await _usuarioService.CreateAsync(user, request.Password, request.Roles);
        }
    }
}
