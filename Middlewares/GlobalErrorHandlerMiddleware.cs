using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using dotnet_rpg.Helpers;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;

namespace dotnet_rpg.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var request = context.Request;
                Console.WriteLine(request.Headers);
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new ServiceResponse<String> { Data = null, Message = error?.Message,Success=false });
                await response.WriteAsync(result);
            }

        }
    }
}