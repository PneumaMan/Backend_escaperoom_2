using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.Usuarios.Queries
{
    public class GetUsuarioByIdQuery : IRequestHandler<GetUsuarioByIdRequest, Response<UsuarioResponse>>
    {
        private readonly IUsuarioRepositoryAsync _usuariosRepository;
        private readonly IAccountService _usuarioService;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        public GetUsuarioByIdQuery(IUsuarioRepositoryAsync usuariosRepository, IMapper mapper, IAccountService usuarioService,
            LanguagesHelper languagesHelper)
        {
            _usuariosRepository = usuariosRepository;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _languagesHelper = languagesHelper;
        }

        public async Task<Response<UsuarioResponse>> Handle(GetUsuarioByIdRequest request, CancellationToken cancellationToken)
        {
            var usuario = await _usuariosRepository.GetUserByIdFullAsync(request.Id);

            if (usuario == null)
            {
                throw new ApiException(this._languagesHelper.UsuarioNoExiste);
            }

            return new Response<UsuarioResponse>(this._mapper.Map<UsuarioResponse>(usuario));
        }
    }
}
