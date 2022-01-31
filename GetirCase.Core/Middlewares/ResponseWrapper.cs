using System.IO;
using System.Net;
using System.Threading.Tasks;
using GetirCase.Core.Models;
using GetirCase.Core.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GetirCase.Core.Middlewares
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await _next(context);

                return;
            }

            using (var buffer = new MemoryStream())
            {
                var stream = context.Response.Body;
                context.Response.Body = buffer;

                await _next.Invoke(context);

                buffer.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(buffer);

                using (var bufferReader = new StreamReader(buffer))
                {
                    string readToEnd = await bufferReader.ReadToEndAsync();
                    if (Helper.IsValidJson(readToEnd))
                    {
                        var objResult = JsonConvert.DeserializeObject(readToEnd);
                        if (objResult != null)
                        {
                            CommonApiResponse result;
                            if (context.Response.StatusCode != 200)
                            {
                                var error = JsonConvert.DeserializeObject<ApiError>(readToEnd);

                                result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, null, error.Message, true, true);
                            }
                            else
                            {
                                result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, "Request successfully executed.");
                            }

                            var jsonString = JsonConvert.SerializeObject(result);

                            await context.Response.WriteAsync(jsonString);
                        }
                    }

                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    await context.Response.Body.CopyToAsync(stream);

                    context.Response.Body = stream;
                }
            }

        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");

        }
    }

    public static class ResponseWrapperExtension
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }
}