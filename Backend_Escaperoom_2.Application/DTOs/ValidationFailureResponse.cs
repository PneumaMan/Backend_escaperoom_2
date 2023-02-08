using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Escaperoom_2.Application.DTOs
{
    public class ValidationFailureResponse
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public object CustomState { get; set; }

        public object AttemptedValue { get; set; }

        public Severity Severity { get; set; }

        public string SeverityName => this.Severity.ToString();

        public string ErrorCode { get; set; }

        /*Constructor*/
        public ValidationFailureResponse()
        {
        }


        public ValidationFailureResponse(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.CustomState = null;
            this.AttemptedValue = null;
            this.Severity = Severity.Error;
            this.ErrorCode = null;
        }

        public ValidationFailureResponse(string propertyName, string errorMessage, object attemptedValue)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.CustomState = null;
            this.AttemptedValue = attemptedValue;
            this.Severity = Severity.Error;
            this.ErrorCode = null;
        }

        public ValidationFailureResponse(string propertyName, string errorMessage, Severity severity = 0, object customState = null, object attemptedValue = null, string errorCode = null)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.CustomState = customState;
            this.AttemptedValue = attemptedValue;
            this.Severity = severity;
            this.ErrorCode = errorCode;
        }
    }
}
