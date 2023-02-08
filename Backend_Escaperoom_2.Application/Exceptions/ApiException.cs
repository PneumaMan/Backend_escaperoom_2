using System;

namespace Backend_Escaperoom_2.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() 
        { 
        }

        public ApiException(string message) : base(message) 
        { 
        }

        public ApiException(string message, params object[] args) : base(String.Format(System.Globalization.CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
