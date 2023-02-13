using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.TipoParticipantes;
using Backend_Escaperoom_2.Application.Helpers;
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

namespace Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries
{
    public class GetAllTiposParticipantesPaginationQuery : IRequestHandler<GetAllTiposParticipantesPaginationRequest, PagedResponse<IEnumerable<TipoParticipanteResponse>>>
    {
        private readonly ITipoParticipantesRepositoryAsync _tipoParticipantesRepositoryAsync;
        private readonly IUrlServices _urlServices;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private IEnumerable<TipoParticipante> _listTiposParticipantes;
        private string _urlFilter;
        private int _count = 0;

        private List<ValidationFailureResponse> _errors;

        public GetAllTiposParticipantesPaginationQuery(ITipoParticipantesRepositoryAsync tipoParticipantesRepositoryAsync, IUrlServices urlServices,
            LanguagesHelper languagesHelper, IMapper mapper)
        {
            _tipoParticipantesRepositoryAsync = tipoParticipantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _urlServices = urlServices;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<PagedResponse<IEnumerable<TipoParticipanteResponse>>> Handle(GetAllTiposParticipantesPaginationRequest request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTipoParticipanteParameter>(request);

            //validad si existe un filtro de busqueda
            await Search(validFilter);

            var menusVM = _mapper.Map<List<TipoParticipanteResponse>>(this._listTiposParticipantes);
            var pageRes = new PagedResponse<IEnumerable<TipoParticipanteResponse>>(menusVM, validFilter.PageNumber, validFilter.PageSize, menusVM.Count(), _count);

            //asignamos las urls del next y del previous
            var next = _urlServices.GetUrlPagination(pageRes, true, _urlFilter);
            pageRes.HasNextPageUrl = pageRes.HasNextPage ? next.ToString() : null;

            var previous = _urlServices.GetUrlPagination(pageRes, false, _urlFilter);
            pageRes.HasPreviuosPageUrl = pageRes.HasPreviuosPage ? previous.ToString() : null;

            return pageRes;
        }

        /*Metodos*/
        private async Task Search(GetAllTipoParticipanteParameter validFilter)
        {
            if (!String.IsNullOrEmpty(validFilter.NombreTipoParticipante) && !String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                this._listTiposParticipantes = await _tipoParticipantesRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.NombreTipo.ToLower().Equals(validFilter.NombreTipoParticipante.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._count = await _tipoParticipantesRepositoryAsync.CountAsync(x => x.NombreTipo.ToLower().Equals(validFilter.NombreTipoParticipante.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._urlFilter += $"&{nameof(validFilter.NombreTipoParticipante)}={validFilter.NombreTipoParticipante}&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            else if (!String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                this._listTiposParticipantes = await _tipoParticipantesRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._count = await _tipoParticipantesRepositoryAsync.CountAsync(x => x.EscapeRoomId.Equals(validFilter.EscapeRoomId));
                this._urlFilter += $"&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            if (!String.IsNullOrEmpty(validFilter.NombreTipoParticipante))
            {
                this._listTiposParticipantes = await _tipoParticipantesRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.NombreTipo.Equals(validFilter.NombreTipoParticipante));
                this._count = await _tipoParticipantesRepositoryAsync.CountAsync(x => x.NombreTipo.Equals(validFilter.NombreTipoParticipante));
                this._urlFilter += $"&{nameof(validFilter.NombreTipoParticipante)}={validFilter.NombreTipoParticipante}";
            }
            else
            {
                this._listTiposParticipantes = await _tipoParticipantesRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
                this._count = await _tipoParticipantesRepositoryAsync.CountAsync();
            }
        }
    }
}