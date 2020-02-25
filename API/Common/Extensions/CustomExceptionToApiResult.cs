using System;
using System.Net;
using Application.Common.Exceptions;

namespace API.Common.Extensions
{
    /* GetApiErrorResult returns error json object with a shape:
     {  error: General message,
        errors: Object of concrete errors }
    */
    public static class CustomExceptionToApiResult
    {
        public static (HttpStatusCode, string) GetApiErrorResult(this Exception exception)
        {
            var apiError = exception switch
            {
                NotFoundException notFoundException => GetNotFoundExceptionResult(notFoundException),
                ValidationException validationException => GetValidationExceptionResult(validationException),
                _ => ApiErrorResult.Default(exception)
            };

            return (apiError.Code, apiError.GetResult());
        }

        private static ApiErrorResult GetNotFoundExceptionResult(NotFoundException notFoundException)
        {
            return new ApiErrorResult
            {
                Code = HttpStatusCode.NotFound,
                Message = notFoundException.Message,
            };
        }

        private static ApiErrorResult GetValidationExceptionResult(ValidationException validationException)
        {
            return new ApiErrorResult
            {
                Code = HttpStatusCode.BadRequest,
                Message = validationException.Message,
                Details = validationException.Failures,
            };
        }
    }
}
