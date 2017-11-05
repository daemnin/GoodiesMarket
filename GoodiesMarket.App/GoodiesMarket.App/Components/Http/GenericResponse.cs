using System.Net;

namespace GoodiesMarket.App.Components.Http
{
    public class GenericResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }
    }
}