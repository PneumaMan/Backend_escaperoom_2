using Backend_Escaperoom_2.Application.DTOs;
using System;
using System.Collections.Generic;

namespace Backend_Escaperoom_2.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationFailureResponse> Errors { get; }

        public ValidationException() : base() 
        { 
        }

        public ValidationException(IEnumerable<ValidationFailureResponse> failures, string message) : base(message)
        {
            Errors = failures;
        }

        public ValidationException(IEnumerable<ValidationFailureResponse> failures) : this()
        {
            Errors = failures;
        }

    }
}
