using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Middleware
{
    public class RequestAndResponseLogginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestAndResponseLogginMiddleware> _logger;
        public RequestAndResponseLogginMiddleware(RequestDelegate next,  ILogger<RequestAndResponseLogginMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
            _logger.LogInformation("Request is: " + request);

            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

               _logger.LogInformation("Response is: " + response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }
    }
}