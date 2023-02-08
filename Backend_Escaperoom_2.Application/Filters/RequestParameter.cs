namespace Backend_Escaperoom_2.Application.Filters
{
    public class RequestParameter
    {
        /*Atributos*/
        private readonly int _defaultPageNumber = 1;
        private readonly int _defaultPageSize = 20;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        /*Constructor*/
        public RequestParameter()
        {
            this.PageNumber = this._defaultPageNumber;
            this.PageSize = this._defaultPageSize;
        }

        public RequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < this._defaultPageNumber ? this._defaultPageNumber : pageNumber;
            this.PageSize = pageSize < this._defaultPageSize ? this._defaultPageSize : pageSize;
        }
    }
}
