using Backend_Escaperoom_2.Application.DTOs;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.Wrappers
{
    public class Response<T>
    {
        /*Atributos*/
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string Path { get; set; }

        public IEnumerable<ValidationFailureResponse> Errors { get; set; }

        /*Constrctor*/
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            IsSuccess = false;
            Message = message;
        }

    }
}
