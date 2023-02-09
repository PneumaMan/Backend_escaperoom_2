using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Participante;
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

namespace Backend_Escaperoom_2.Application.Features.WebApi.Participantes.Queries
{
    public class GetAllParticipantesPaginationQuery : IRequestHandler<GetAllParticipantesPaginationRequest, PagedResponse<IEnumerable<ParticipanteResponse>>>
    {
        private readonly IParticipantesRepositoryAsync _participantesRepositoryAsync;
        private readonly IUrlServices _urlServices;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private IEnumerable<Participante> _listParticipantes;
        private string _urlFilter;
        private int _count = 0;

        private List<ValidationFailureResponse> _errors;

        public GetAllParticipantesPaginationQuery(IParticipantesRepositoryAsync participantesRepositoryAsync, IUrlServices urlServices,
             LanguagesHelper languagesHelper, IMapper mapper)
        {
            _participantesRepositoryAsync = participantesRepositoryAsync;
            _languagesHelper = languagesHelper;
            _urlServices = urlServices;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<PagedResponse<IEnumerable<ParticipanteResponse>>> Handle(GetAllParticipantesPaginationRequest request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllParticipantesParameter>(request);

            //validad si existe un filtro de busqueda
            await Search(validFilter);

            var menusVM = _mapper.Map<List<ParticipanteResponse>>(_listParticipantes);
            var pageRes = new PagedResponse<IEnumerable<ParticipanteResponse>>(menusVM, validFilter.PageNumber, validFilter.PageSize, menusVM.Count(), _count);

            //asignamos las urls del next y del previous
            var next = _urlServices.GetUrlPagination(pageRes, true, _urlFilter);
            pageRes.HasNextPageUrl = pageRes.HasNextPage ? next.ToString() : null;

            var previous = _urlServices.GetUrlPagination(pageRes, false, _urlFilter);
            pageRes.HasPreviuosPageUrl = pageRes.HasPreviuosPage ? previous.ToString() : null;

            return pageRes;
        }

        /*Metodos*/
        private async Task Search(GetAllParticipantesParameter validFilter)
        {
            if (!String.IsNullOrEmpty(validFilter.NombreParticipante) && !String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                _listParticipantes = await _participantesRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize, x => x.Nombres.ToLower().Equals(validFilter.NombreParticipante.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                _count = await _participantesRepositoryAsync.CountAsync(x => x.Nombres.ToLower().Equals(validFilter.NombreParticipante.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                _urlFilter += $"&{nameof(validFilter.NombreParticipante)}={validFilter.NombreParticipante}&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            else if (!String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                _listParticipantes = await _participantesRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize, x => x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                _count = await _participantesRepositoryAsync.CountAsync(x => x.EscapeRoomId.Equals(validFilter.EscapeRoomId));
                _urlFilter += $"&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            else if (!String.IsNullOrEmpty(validFilter.NombreParticipante))
            {
                _listParticipantes = await _participantesRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize, x => x.Nombres.ToLower().Equals(validFilter.NombreParticipante.ToLower()));
                _count = await _participantesRepositoryAsync.CountAsync(x => x.Nombres.ToLower().Equals(validFilter.NombreParticipante.ToLower()));
                _urlFilter += $"&{nameof(validFilter.NombreParticipante)}={validFilter.NombreParticipante}";
            }
            else
            {
                _listParticipantes = await _participantesRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize);
                _count = await _participantesRepositoryAsync.CountAsync();
            }
        }
    }
}