using GoodiesMarket.Components.Models;
using System;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Contracts
{
    public interface IHttpClient : IDisposable
    {
        Task<Result> Delete(string requestUrl);
        Task<Result> Put(string requestUrl, string body);
        Task<Result> Post(string requestUrl, string body, string contentType = null);
        Task<Result> Get(string requestUrl);
    }
}
