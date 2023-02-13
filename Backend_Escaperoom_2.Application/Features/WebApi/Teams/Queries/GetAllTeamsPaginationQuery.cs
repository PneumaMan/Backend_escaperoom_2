using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.Teams;
using Backend_Escaperoom_2.Application.Features.WebApi.TipoParticipantes.Queries;
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

namespace Backend_Escaperoom_2.Application.Features.WebApi.Teams.Queries
{
    public class GetAllTeamsPaginationQuery : IRequestHandler<GetAllTeamsPaginationRequest, PagedResponse<IEnumerable<TeamResponse>>>
    {
        private readonly ITeamsRepositoryAsync _TeamsRepositoryAsync;
        private readonly IUrlServices _urlServices;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private IEnumerable<Team> _listTeams;
        private string _urlFilter;
        private int _count = 0;

        private List<ValidationFailureResponse> _errors;

        public GetAllTeamsPaginationQuery(ITeamsRepositoryAsync TeamsRepositoryAsync, IUrlServices urlServices,
            LanguagesHelper languagesHelper, IMapper mapper)
        {
            _TeamsRepositoryAsync = TeamsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _urlServices = urlServices;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<PagedResponse<IEnumerable<TeamResponse>>> Handle(GetAllTeamsPaginationRequest request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTeamParameter>(request);

            //validad si existe un filtro de busqueda
            await Search(validFilter);

            var menusVM = _mapper.Map<List<TeamResponse>>(this._listTeams);
            var pageRes = new PagedResponse<IEnumerable<TeamResponse>>(menusVM, validFilter.PageNumber, validFilter.PageSize, menusVM.Count(), _count);

            //asignamos las urls del next y del previous
            var next = _urlServices.GetUrlPagination(pageRes, true, _urlFilter);
            pageRes.HasNextPageUrl = pageRes.HasNextPage ? next.ToString() : null;

            var previous = _urlServices.GetUrlPagination(pageRes, false, _urlFilter);
            pageRes.HasPreviuosPageUrl = pageRes.HasPreviuosPage ? previous.ToString() : null;

            return pageRes;
        }

        /*Metodos*/
        private async Task Search(GetAllTeamParameter validFilter)
        {
            if (!String.IsNullOrEmpty(validFilter.NombreTeam) && !String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                this._listTeams = await _TeamsRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.NombreTeam.ToLower().Equals(validFilter.NombreTeam.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._count = await _TeamsRepositoryAsync.CountAsync(x => x.NombreTeam.ToLower().Equals(validFilter.NombreTeam.ToLower()) && x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._urlFilter += $"&{nameof(validFilter.NombreTeam)}={validFilter.NombreTeam}&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            else if (!String.IsNullOrEmpty(validFilter.EscapeRoomId))
            {
                this._listTeams = await _TeamsRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.EscapeRoomId == Convert.ToInt32(validFilter.EscapeRoomId));
                this._count = await _TeamsRepositoryAsync.CountAsync(x => x.EscapeRoomId.Equals(validFilter.EscapeRoomId));
                this._urlFilter += $"&{nameof(validFilter.EscapeRoomId)}={validFilter.EscapeRoomId}";
            }
            if (!String.IsNullOrEmpty(validFilter.NombreTeam))
            {
                this._listTeams = await _TeamsRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, x => x.NombreTeam.Equals(validFilter.NombreTeam));
                this._count = await _TeamsRepositoryAsync.CountAsync(x => x.NombreTeam.Equals(validFilter.NombreTeam));
                this._urlFilter += $"&{nameof(validFilter.NombreTeam)}={validFilter.NombreTeam}";
            }
            else
            {
                this._listTeams = await _TeamsRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
                this._count = await _TeamsRepositoryAsync.CountAsync();
            }
        }
    }
}