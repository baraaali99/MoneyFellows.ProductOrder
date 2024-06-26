﻿using Serilog;
using System.Net;

namespace MoneyFellows.ProductOrder.API.Infrastructure.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        private static void HandleException(Exception exception)
        {
            Log.Error(exception, "An error Occured");
        }
    }
}
