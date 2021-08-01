using OrderApi.Services.v1;
using OrderApi.Services.v1.Exceptions;
using OrderApi.Services.v1.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderApi.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = context.Response;
            var responseModel = new Response<string>() { Succeeded = false, Message = exception?.Message };
            
            switch (exception)
            {
                case ApiException e:
                    // custom application error
                    code = HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    // custom application error
                    code = HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            var result = JsonSerializer.Serialize(responseModel);

            return context.Response.WriteAsync(result);
        }
    }
}