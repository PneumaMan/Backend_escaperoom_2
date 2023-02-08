using Backend_Escaperoom_2.Application.Wrappers;
using System;

namespace Backend_Escaperoom_2.Application.Interfaces.Services
{
    public interface IUrlServices
    {
        string GetUrlBase();

        Uri GetUrlPagination<T>(PagedResponse<T> page, bool isNextPage, string UrlFilters = null);
    }
}
