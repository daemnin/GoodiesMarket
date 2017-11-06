using System.Net;

namespace GoodiesMarket.Components.Http
{
    public class BaseResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }
    }
}
