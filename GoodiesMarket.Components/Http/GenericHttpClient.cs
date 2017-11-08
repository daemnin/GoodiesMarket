using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Contracts;
using GoodiesMarket.Components.Helpers;
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
        private bool isAuthorized;

        public GenericHttpClient(string baseAddress, bool isAuthorized, params Tuple<string, string>[] headers)
        {
            BaseAddress = new Uri(baseAddress);
            this.isAuthorized = isAuthorized;

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
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            Result response = await CreateRequest(() => SendAsync(request), headers);

            return response;
        }

        public async Task<Result> Put(string requestUrl, string body, IEnumerable<Tuple<string, string>> headers = null)
        {
            var stringContent = new StringContent(body, Encoding.UTF8, ContentType.JSON);

            Result response = await CreateRequest(() => PutAsync(requestUrl, stringContent), headers);

            return response;
        }

        public async Task<Result> Post(string requestUrl, string body, string contentType = null, IEnumerable<Tuple<string, string>> headers = null)
        {
            var stringContent = new StringContent(body, Encoding.UTF8, contentType ?? ContentType.JSON);

            Result response = await CreateRequest(() => PostAsync(requestUrl, stringContent), headers);

            return response;
        }

        public async Task<Result> Get(string requestUrl, IEnumerable<Tuple<string, string>> headers = null)
        {
            Result response = await CreateRequest(() => GetAsync(requestUrl), headers);

            return response;
        }

        private async Task ConfigureHeaders(IEnumerable<Tuple<string, string>> headers = null)
        {
            if (isAuthorized && CredentialsHelper.HasSession)
            {
                var expiration = CredentialsHelper.ExpirationDate?.AddMinutes(-5);

                var validCredentials = DateTime.Now < CredentialsHelper.ExpirationDate?.AddMinutes(-5);

                if (!validCredentials)
                {
                    validCredentials = await RefreshCredentials();
                }

                if (validCredentials)
                {
                    DefaultRequestHeaders.Authorization = CredentialsHelper.AuthorizationHeader;
                }
            }

            foreach (var header in headers ?? new List<Tuple<string, string>>())
            {
                DefaultRequestHeaders.Add(header.Item1, header.Item2);
            }
        }

        private async Task<Result> CreateRequest(Func<Task<HttpResponseMessage>> httpCall, IEnumerable<Tuple<string, string>> headers = null)
        {
            await ConfigureHeaders(headers);

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

        private async Task<bool> RefreshCredentials()
        {
            bool validCredentials = false;
            var requestUrl = Constants.REFRESH_TOKEN_CALL;

            var request = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("refresh_token", CredentialsHelper.RefreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", Constants.APP_CLIENT_ID)
            };

            var body = await new FormUrlEncodedContent(request).ReadAsStringAsync();

            var requestBody = new StringContent(body, Encoding.UTF8, ContentType.WWW_FORM);

            var response = await PostAsync(requestUrl, requestBody);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                CredentialsHelper.RegisterSignIn(JToken.Parse(jsonResponse));
                validCredentials = true;
            }
            else
            {
                CredentialsHelper.LogOut();
            }

            return validCredentials;
        }
    }
}
