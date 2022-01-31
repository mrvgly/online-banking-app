using System;
using System.Net;
using System.Runtime.Serialization;

namespace GetirCase.Core.Models
{
    public class CommonApiResponse
    {
        public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null, bool isError = false, bool showData = false, string version = "v1")
        {
            return new CommonApiResponse(statusCode, result, errorMessage, isError, showData, version);
        }

        public CommonApiResponse()
        {

        }

        public string Version { get; set; }

        public string RequestId { get; }

        public int StatusCode { get; set; }

        public bool IsError { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        protected CommonApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null, bool isError = false, bool showData = false, string version = "v1")
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            if (!isError)
                Data = result;

            if (isError && showData)
                Data = result;

            Message = errorMessage;
            IsError = isError;
            Version = version;
        }
    }
}
