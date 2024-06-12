using System.Net;

namespace SWD.NextIntern.Service.DTOs.Responses
{
    public class ResponseObject<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }

        public ResponseObject(T? data, HttpStatusCode status, string message)
        {
            Data = data;
            Status = status;
            Message = message;
        }

        public ResponseObject(HttpStatusCode status, string message)
        {
            Data = default;
            Status = status;
            Message = message;
        }
    }
}