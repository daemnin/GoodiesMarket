using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoodiesMarket.App.Components.Http
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

        public async Task<GenericResponse> Delete(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            GenericResponse response = await CreateRequest(() => SendAsync(request));

            return response;
        }

        public async Task<GenericResponse> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, jsonContentType);

            GenericResponse response = await CreateRequest(() => PutAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<GenericResponse> Post(string requestUrl, string body, string contentType = jsonContentType, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            var stringContent = new StringContent(body, Encoding.UTF8, contentType);

            GenericResponse response = await CreateRequest(() => PostAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<GenericResponse> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            ConfigureHeaders(headers);

            GenericResponse response = await CreateRequest(() => GetAsync(requestUrl));

            return response;
        }

        private void ConfigureHeaders(IEnumerable<Tuple<string, string>> headers = null)
        {
            foreach (var header in headers ?? new List<Tuple<string, string>>())
            {
                DefaultRequestHeaders.Add(header.Item1, header.Item2);
            }
        }

        private async Task<GenericResponse> CreateRequest(Func<Task<HttpResponseMessage>> httpCall)
        {
            GenericResponse result = new GenericResponse();

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