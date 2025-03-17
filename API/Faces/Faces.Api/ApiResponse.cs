using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Faces.Api
{
    public class ApiResponse<TResult> : IActionResult
    {

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<TResult> Data { get; set; } = new List<TResult>();

        public ApiResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiResponse(HttpStatusCode statusCode, string message) : this(statusCode)
        {
            Message = message;
        }

        public ApiResponse(HttpStatusCode statusCode, string message, TResult result) : this(statusCode, message)
        {
            if(result != null)
                Data.Add(result);
        }

        public ApiResponse(HttpStatusCode statusCode, string message, IEnumerable<TResult> result) : this(statusCode, message)
        {
            Data.AddRange(result);
        }


        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = (int)StatusCode;
            response.ContentType = "application/json";

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            await response.WriteAsync(JsonSerializer.Serialize(this, options));
        }
    }
}
