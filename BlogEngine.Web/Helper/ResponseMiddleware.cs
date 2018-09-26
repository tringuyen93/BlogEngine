using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BlogEngine.WebApi.ViewModel;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VMD.RESTApiResponseWrapper.Core.Extensions;

namespace BlogEngine.WebApi.Helper
{
    public static class ResponseMiddlewareExtention
    {
        public static IApplicationBuilder ResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMiddleware>();
        }
    }
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context) || IsAuthorization(context)) await _next(context);
            else
            {
                var originalStream = context.Response.Body;
                using (var body = new MemoryStream())
                {
                    context.Response.Body = body;
                    try
                    {
                        await _next.Invoke(context);
                        if (context.Response.StatusCode == (int) HttpStatusCode.OK)
                        {
                            var s = await FormatResponse(context.Response);
                            await SuccessRequest(context, s, context.Response.StatusCode);
                        }
                        else
                        {
                            await NotSuccessRequestAsync(context, context.Response.StatusCode);
                        }
                    }
                    catch (Exception e)
                    {
                        await ExceptionAsync(context, e);
                    }
                    finally
                    {
                        body.Seek(0, SeekOrigin.Begin);
                        await body.CopyToAsync(originalStream);
                    }
                }
            }
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }
        private bool IsAuthorization(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/connect");
        }

        private static Task ExceptionAsync(HttpContext context, Exception exception)
        {
            ValidationResultModel error = null;
            ResponseMetadata apiResponse = null;
            int code = 0;

            if (exception is CustomException)
            {
                var ex = exception as CustomException;
                error = new ValidationResultModel(ex.Message);
                error.ValidationErrors = ex.Erros;
                code = ex.StatusCode;
                context.Response.StatusCode = code;

            }
            else if (exception is UnauthorizedAccessException)
            {
                error = new ValidationResultModel("Unauthorized Access");
                code = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
                #if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
                #else
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
                #endif
                error = new ValidationResultModel(msg) {ExceptionMessage = stack};
                code = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }
            context.Response.ContentType = "application/json";
            apiResponse = new ResponseMetadata(code, ResponseEnum.Exception.GetDescription(), null, error);
            var json = JsonConvert.SerializeObject(apiResponse);
            return context.Response.WriteAsync(json);
        }

        private static Task NotSuccessRequestAsync(HttpContext context, int statusCode)
        {
            context.Response.ContentType = "application/json";
            ValidationResultModel error = null;
            ResponseMetadata response = null;

            if (statusCode == (int)HttpStatusCode.NotFound)
                error = new ValidationResultModel("The specified URI does not exist. Please verify and try again.");
            else if (statusCode == (int)HttpStatusCode.NoContent)
                error = new ValidationResultModel("The specified URI does not contain any content.");
            else
                error = new ValidationResultModel("Your request cannot be processed. Please contact a support.");
            
            response = new ResponseMetadata(statusCode, ResponseEnum.Failure.GetDescription(), null, error);
            context.Response.StatusCode = statusCode;
            var json = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(json);
        }

        private static Task SuccessRequest(HttpContext context, object body, int statusCode)
        {
            context.Response.ContentType = "application/json";
            string jsonString, bodyText = String.Empty;
            ResponseMetadata response = null;
            if (!body.ToString().IsValidJson())
            {
                bodyText = JsonConvert.SerializeObject(body);
            }
            else
            {
                bodyText = body.ToString();
            }
            object bodyContent = JsonConvert.DeserializeObject<object>(bodyText);
            response = new ResponseMetadata(statusCode, ResponseEnum.Success.GetDescription(), bodyContent, null);
            jsonString = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(jsonString);
        }

        private async Task<string> FormatResponse(HttpResponse contextResponse)
        {
            contextResponse.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(contextResponse.Body).ReadToEndAsync();
            contextResponse.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }
}
