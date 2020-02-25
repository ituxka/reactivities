using System;
using System.Net;
using System.Text.Json;

namespace API.Common.Models
{
    public class ApiErrorResult
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public object Details  = new object();

        public string GetResult()
        {
            return JsonSerializer.Serialize(new
            {
                error = Message,
                errors = Details,
            });
        }

        public static ApiErrorResult Default(Exception exception)
        {
            return new ApiErrorResult
            {
                Code = HttpStatusCode.InternalServerError,
                Message = exception.Message
            };
        }
    }
}
