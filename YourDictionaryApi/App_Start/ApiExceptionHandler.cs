using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using IdentityServer4.Extensions;

namespace YourDictionaryApi.App_Start
{
    public class ApiExceptionHandler
    {

        /// <summary>
        /// The default language identifier.
        /// </summary>
        protected const string DefaultLanguageId = "en";

        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>Task.</returns>

        public async Task HandleAsync(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            HttpStatusCode statusCode;
            string message = string.Empty;
            //var errors = new List<ErrorResultBase>();

            if (feature.Error is Exception)
            {
                var exception = feature.Error as Exception;

                if (!context.User.IsAuthenticated())
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "You are Unauthorized (HandleAsync)";

                }
                else
                {
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Something went wrong (HandleAsync)";
                }

            }
            else
            {
                var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                var exception = feature.Error;
                var errorMessage = environment.IsDevelopment() ? GetErrorMessage(exception) : GetErrorMessage(ErrorCode.GeneralException);

                statusCode = HttpStatusCode.InternalServerError;
            }

            await WriteResponseAsync(context, statusCode, MediaTypeNames.Application.Json, message);
        }//, ILog logger); //using log4net;



        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The error message.</returns>
        protected static string GetErrorMessage(System.Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return $"{exception.Message}{Environment.NewLine}{exception.StackTrace}";
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>System.String.</returns>
        protected static string GetErrorMessage(ErrorCode errorCode)
        {
            //return ErrorTexts.Data.ContainsKey(errorCode) ? ErrorTexts.Data[errorCode] : errorCode.ToString();
            return errorCode.ToString();
        }

        /// <summary>
        /// Write response as an asynchronous operation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="responseData">The response data.</param>
        /// <returns>Task.</returns>
        protected static async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, string contentType, object responseData)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = new MediaTypeHeaderValue(contentType).ToString();

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(responseData, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented }),
                Encoding.UTF8);
        }



        public enum ErrorCode
        {
            /// <summary>
            /// The general exception
            /// </summary>
            GeneralException = 1,
        }
    }
}
