using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Http
{
    public class GenericHttpClient : HttpClient, IHttpClient
    {
        private const string jsonContentType = "application/json";

        public GenericHttpClient(string baseAddress, params Tuple<string, string>[] headers)
        {
            BaseAddress = new Uri(baseAddress);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    DefaultRequestHeaders.Add(header.Item1, header.Item2);
                }
            }
        }

        public async Task<Result> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            Result response = await CreateRequest(() => SendAsync(request));

            return response;
        }

        public async Task<Result> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, jsonContentType);

            Result response = await CreateRequest(() => PutAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<Result> Post(string requestUrl, string body, string contentType = null, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, contentType ?? jsonContentType);

            Result response = await CreateRequest(() => PostAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<Result> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            Result response = await CreateRequest(() => GetAsync(requestUrl));

            return response;
        }

        private void ConfigureHeaders(IEnumerable<Tuple<string, string>> headers = null)
        {
            foreach (var header in headers ?? new List<Tuple<string, string>>())
            {
                DefaultRequestHeaders.Add(header.Item1, header.Item2);
            }
        }

        private async Task<Result> CreateRequest(Func<Task<HttpResponseMessage>> httpCall)
        {
            HttpResponseMessage response = await httpCall();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var result = new Result
            {
                Response = JToken.Parse(jsonResponse),
                Status = response.StatusCode,
                Succeeded = response.StatusCode == HttpStatusCode.OK
            };

            return result;
        }
    }
}
