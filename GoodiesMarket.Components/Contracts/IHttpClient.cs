using GoodiesMarket.Components.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Contracts
{
    public interface IHttpClient : IDisposable
    {
        Task<Result> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
        Task<Result> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null);
        Task<Result> Post(string requestUrl, string body, string contentType = null, IEnumerable<Tuple<string, string>> headers = null);
        Task<Result> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null);
    }
}
