using GoodiesMarket.Components.Contracts;
using System;
using System.Collections.Generic;
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

        public async Task<BaseResponse> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            BaseResponse response = await CreateRequest(() => SendAsync(request));

            return response;
        }

        public async Task<BaseResponse> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, jsonContentType);

            BaseResponse response = await CreateRequest(() => PutAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<BaseResponse> Post(string requestUrl, string body, string contentType = jsonContentType, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, contentType);

            BaseResponse response = await CreateRequest(() => PostAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<BaseResponse> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            BaseResponse response = await CreateRequest(() => GetAsync(requestUrl));

            return response;
        }

        private void ConfigureHeaders(IEnumerable<Tuple<string, string>> headers = null)
        {
            foreach (var header in headers ?? new List<Tuple<string, string>>())
            {
                DefaultRequestHeaders.Add(header.Item1, header.Item2);
            }
        }

        private async Task<BaseResponse> CreateRequest(Func<Task<HttpResponseMessage>> httpCall)
        {
            BaseResponse result = new BaseResponse();

            try
            {
                HttpResponseMessage response = await httpCall();

                response.EnsureSuccessStatusCode();
                result.StatusCode = response.StatusCode;
                result.Message = response.ReasonPhrase;

                string jsonResponse = await response.Content.ReadAsStringAsync();
                result.Response = jsonResponse;
            }
            catch (Exception ex)
            {
                result.Response = ex.Message;
            }

            return result;
        }
    }
}
