using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Backend_Escaperoom_2.Application.DTOs;
using Backend_Escaperoom_2.Application.DTOs.WebApi;
using Backend_Escaperoom_2.Application.Exceptions;
using Backend_Escaperoom_2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if ((context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/swagger/index.html"))
                    && !context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<ValidationFailureResponse>() { IsSuccess = false, Message = error?.Message, Path = context.Request.Path };

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                if (error.InnerException != null)
                {
                    this._logger.LogInformation(error, $"Error: {error.Message} - Error específico: { error.InnerException.Message }.");
                }
                else
                {
                    this._logger.LogInformation(error, $"Error: {error.Message}.");
                }

                await response.WriteAsync(result);
            }
        }
    }
}
