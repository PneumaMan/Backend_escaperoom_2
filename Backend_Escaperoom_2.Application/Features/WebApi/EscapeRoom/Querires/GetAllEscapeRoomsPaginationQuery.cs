using AutoMapper;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi.EscapeRoom;
using Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Queries;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces.Repositories;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Application.Features.WebApi.EscapeRoom.Querires
{
    public class GetAllEscapeRoomPaginationQuery : IRequestHandler<GetAllEscapeRoomsPaginationRequest, PagedResponse<IEnumerable<EscapeRoomResponse>>>
    {
        private readonly IEscapeRoomsRepositoryAsync _escapeRoomsRepositoryAsync;
        private readonly IUrlServices _urlServices;
        private readonly IMapper _mapper;
        private readonly LanguagesHelper _languagesHelper;

        private IEnumerable<Domain.Entities.EscapeRoom> _listEscapeRooms;
        private string _urlFilter;
        private int _count = 0;

        private List<ValidationFailureResponse> _errors;

        public GetAllEscapeRoomPaginationQuery(IEscapeRoomsRepositoryAsync escapeRoomsRepositoryAsync, IUrlServices urlServices,
            LanguagesHelper languagesHelper, IMapper mapper)
        {
            _escapeRoomsRepositoryAsync = escapeRoomsRepositoryAsync;
            _languagesHelper = languagesHelper;
            _urlServices = urlServices;
            _mapper = mapper;

            _errors = new List<ValidationFailureResponse>();
        }

        public async Task<PagedResponse<IEnumerable<EscapeRoomResponse>>> Handle(GetAllEscapeRoomsPaginationRequest request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllEscapeRoomParameter>(request);

            //validad si existe un filtro de busqueda
            await Search(validFilter);

            var menusVM = _mapper.Map<List<EscapeRoomResponse>>(_listEscapeRooms);
            var pageRes = new PagedResponse<IEnumerable<EscapeRoomResponse>>(menusVM, validFilter.PageNumber, validFilter.PageSize, menusVM.Count(), _count);

            //asignamos las urls del next y del previous
            var next = _urlServices.GetUrlPagination(pageRes, true, _urlFilter);
            pageRes.HasNextPageUrl = pageRes.HasNextPage ? next.ToString() : null;

            var previous = _urlServices.GetUrlPagination(pageRes, false, _urlFilter);
            pageRes.HasPreviuosPageUrl = pageRes.HasPreviuosPage ? previous.ToString() : null;

            return pageRes;
        }

        /*Metodos*/
        private async Task Search(GetAllEscapeRoomParameter validFilter)
        {
            if (!string.IsNullOrEmpty(validFilter.NombreEscapeRoom))
            {
                _listEscapeRooms = await _escapeRoomsRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize, x => x.NombreEscapeRoom.Equals(validFilter.NombreEscapeRoom));
                _count = await _escapeRoomsRepositoryAsync.CountAsync(x => x.NombreEscapeRoom.Equals(validFilter.NombreEscapeRoom));
                _urlFilter += $"&{nameof(validFilter.NombreEscapeRoom)}={validFilter.NombreEscapeRoom}";
            }
            else
            {
                _listEscapeRooms = await _escapeRoomsRepositoryAsync.GetPagedReponseFullAsync(validFilter.PageNumber, validFilter.PageSize);
                _count = await _escapeRoomsRepositoryAsync.CountAsync();
            }
        }
    }
}