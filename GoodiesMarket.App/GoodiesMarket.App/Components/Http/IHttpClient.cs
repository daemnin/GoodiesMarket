using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodiesMarket.App.Components.Http
{
    public interface IHttpClient : IDisposable
    {
        Task<GenericResponse> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
        Task<GenericResponse> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null);
        Task<GenericResponse> Post(string requestUrl, string body, string contentType = null, IEnumerable<Tuple<string, string>> headers = null);
        Task<GenericResponse> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
    }
}