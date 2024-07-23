using System.Diagnostics;

namespace Para.Api.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path; 
            System.Console.WriteLine(message);

            await _next(context);
            watch.Stop();

            message = "[Response]  HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.ElapsedMilliseconds + " ms.";
            System.Console.WriteLine(message);
        }
    }
}
