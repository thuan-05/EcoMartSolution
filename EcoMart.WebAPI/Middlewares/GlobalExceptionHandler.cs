using EcoMart.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EcoMart.WebAPI.Middlewares
{
    /// <summary>
    /// Bắt mọi exception từ Service và trả về HTTP response JSON chuẩn.
    /// Đăng ký tại Program.cs: AddExceptionHandler + UseExceptionHandler.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            var (statusCode, message) = exception switch
            {
                DuplicateUsernameException  => (StatusCodes.Status400BadRequest,          exception.Message),
                InvalidCredentialsException => (StatusCodes.Status401Unauthorized,         exception.Message),
                KeyNotFoundException        => (StatusCodes.Status404NotFound,             exception.Message),
                _                           => (StatusCodes.Status500InternalServerError,  "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau.")
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title  = message
            }, cancellationToken);

            return true;
        }
    }
}
