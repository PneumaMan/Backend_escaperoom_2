using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;

namespace Backend_Escaperoom_2.Infrastruture.Shared.Services
{
    public class UrlServices : IUrlServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUrlBase()
        {
            var request = this._httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        public Uri GetUrlPagination<T>(PagedResponse<T> page, bool isNextPage, string UrlFilters = null)
        {
            var urlBase = $"{this.GetUrlBase()}{this._httpContextAccessor.HttpContext.Request.Path}";
            var urlPagination = String.Empty;

            //validamos si es next page o previus page
            if (isNextPage && page.HasNextPage)
            {
                urlPagination += $"{nameof(page.PageNumber)}={page.NextPageNumber}&{nameof(page.PageSize)}={page.PageSize}";
            }
            else if (!isNextPage && page.HasPreviuosPage)
            {
                urlPagination += $"{nameof(page.PageNumber)}={page.PreviuosPageNumber}&{nameof(page.PageSize)}={page.PageSize}";
            }
            else
            {
                return null;
            }

            //vaidamos filtros
            if (UrlFilters != null)
            {
                return new Uri($"{urlBase}?{urlPagination}{UrlFilters}");
            }

            return new Uri($"{urlBase}?{urlPagination}");
        }

    }
}
