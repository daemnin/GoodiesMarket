using GoodiesMarket.Components.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Contracts
{
    public interface IHttpClient : IDisposable
    {
        Task<BaseResponse> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
        Task<BaseResponse> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null);
        Task<BaseResponse> Post(string requestUrl, string body, string contentType = null, IEnumerable<Tuple<string, string>> headers = null);
        Task<BaseResponse> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
    }
}
