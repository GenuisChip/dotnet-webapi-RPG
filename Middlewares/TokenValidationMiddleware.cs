using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Net;

namespace dotnet_rpg.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = null;
            var request = context.Request;

            request.Headers.ToList().ForEach(v =>
            {
                if (v.Key == "Authorization")
                {
                    token = v.Value.ToString().Substring(6);
                }
            });

            if (token == null)
            {
                var result = JsonSerializer.Serialize(new ServiceResponse<String> { Success = false, Message = "Invalid Token" });
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await response.WriteAsync(result);
            }
            else
            {

                await _next(context);
            }

        }
    }
}