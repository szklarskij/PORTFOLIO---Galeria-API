﻿
using GaleriaT_API.Exceptions;

namespace GaleriaT_API.Middleware

{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next.Invoke(context);
            }

            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;

            }
            catch (BadRequestExcepion badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (BadImageException badImageException)
            {
                context.Response.StatusCode = 599;
                await context.Response.WriteAsync(badImageException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);

            }

            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
