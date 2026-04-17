using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[ERROR] Unauthorized: {ex.Message}");
                await WriteResponse(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[ERROR] InvalidOp: {ex.Message}");
                await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Unhandled: {ex.Message}");
                await WriteResponse(context, HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            var response = new ErrorResponse { Message = message, StatusCode = (int)code };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
