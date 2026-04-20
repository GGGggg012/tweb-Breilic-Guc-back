using System;
using System.Net;
using System.Text.Json;
using eUseControl.Model;
using Microsoft.AspNetCore.Http;

namespace eUseControl.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context)
        {
            try
            {
                _next(context).GetAwaiter().GetResult();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[ERROR] Unauthorized: {ex.Message}");
                WriteResponse(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[ERROR] InvalidOp: {ex.Message}");
                WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Unhandled: {ex.Message}");
                WriteResponse(context, HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        private static void WriteResponse(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            var response = new ErrorResponse { Message = message, StatusCode = (int)code };
            var json = JsonSerializer.Serialize(response);
            context.Response.WriteAsync(json).GetAwaiter().GetResult();
        }
    }
}
