using System.Net;

namespace CoreApp.Lib
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public APIResponse(HttpStatusCode statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
