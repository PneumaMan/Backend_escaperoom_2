using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios.CRUD;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Usuarios;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using Backend_Escaperoom_2.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.Usuarios.Queries
{
    public class GetAllUsuariosQuery : IRequestHandler<GetAllUsuariosRequest, PagedResponse<IEnumerable<UsuarioResponse>>>
    {
        private readonly IUsuarioRepositoryAsync _usuariosRepository;
        private readonly IUrlServices _urlServices;
        private readonly IMapper _mapper;

        private IEnumerable<Usuario> _listUsuarios;
        private string _urlFilter = String.Empty;

        public GetAllUsuariosQuery(IUsuarioRepositoryAsync usuariosRepository, IUrlServices urlServices,
            IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _urlServices = urlServices;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<UsuarioResponse>>> Handle(GetAllUsuariosRequest request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllUsuariosParameter>(request);

            //validad si existe un filtro de busqueda
            await this.Search(validFilter);

            var totalCount = await _usuariosRepository.CountUsuariosAsync();
            var UsuarioVM = _mapper.Map<IEnumerable<UsuarioResponse>>(this._listUsuarios);
            var pageRes = new PagedResponse<IEnumerable<UsuarioResponse>>(UsuarioVM, validFilter.PageNumber, validFilter.PageSize, UsuarioVM.Count(), totalCount);

            //asignamos las urls del next y del previous
            var next = this._urlServices.GetUrlPagination(pageRes, true, this._urlFilter);
            pageRes.HasNextPageUrl = pageRes.HasNextPage ? next.ToString() : null;

            var previous = this._urlServices.GetUrlPagination(pageRes, false, this._urlFilter);
            pageRes.HasPreviuosPageUrl = pageRes.HasPreviuosPage ? previous.ToString() : null;

            return pageRes;
        }

        private async Task Search(GetAllUsuariosParameter validFilter)
        {
            this._listUsuarios = await _usuariosRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
