using System.Net;

namespace ContactManagementSystemAPI
{
    public class ResponseMessage
    {
        public object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
