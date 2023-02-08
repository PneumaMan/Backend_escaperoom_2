using System;

namespace Backend_Escaperoom_2.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        /*Atributos*/
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRegisters { get; set; }
        public int RegistersForPage { get; set; }
        public int TotalPages => this.TotalRegisters >= this.PageSize ? (int)Math.Ceiling((double)this.TotalRegisters / (double)this.PageSize) : 1;
        public bool HasPreviuosPage => this.PageNumber > 1;
        public int? PreviuosPageNumber => this.HasPreviuosPage ? this.PageNumber - 1 : (int?)null;
        public string HasPreviuosPageUrl { get; set; }
        public bool HasNextPage => this.PageNumber < this.TotalPages;
        public int? NextPageNumber => this.HasNextPage ? this.PageNumber + 1 : (int?)null;
        public string HasNextPageUrl { get; set; }
        public int MaxSize => this.TotalPages < 5 ? this.TotalPages : 5;
        public int TotalRegistrosToPage => (this.PageNumber-1) * this.PageSize + this.RegistersForPage;

        /*Constructores*/
        public PagedResponse()
        {
        }


        public PagedResponse(T data, int pageNumber, int pageSize, int registersForPage, int totalRegisters,
            string hasPreviuosPageUrl = null, string hasNextPageUrl = null, string message = null)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.RegistersForPage = registersForPage;
            this.TotalRegisters = totalRegisters;
            this.HasPreviuosPageUrl = hasPreviuosPageUrl;
            this.HasNextPageUrl = hasNextPageUrl;
            this.Data = data;
            this.Message = message;
            this.IsSuccess = true;
            this.Errors = null;
        }

        public PagedResponse(int pageNumber, int pageSize, int totalCountPage, int totalCount, string message)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.RegistersForPage = totalCountPage;
            this.TotalRegisters = totalCount;
            this.Message = message;
            this.IsSuccess = false;
            this.Errors = null;
        }


    }
}
