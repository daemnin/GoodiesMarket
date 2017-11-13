using GoodiesMarket.Components.Configs;
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
        private Contracts.ICredentials credentials;

        public GenericHttpClient(string baseAddress) : this(baseAddress, null) { }

        public GenericHttpClient(string baseAddress, Contracts.ICredentials credentials)
            : base()
        {
            BaseAddress = new Uri(baseAddress);
            this.credentials = credentials;
        }

        public async Task<Result> Delete(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            Result response = await CreateRequest(() => SendAsync(request));

            return response;
        }

        public async Task<Result> Put(string requestUrl, string body)
        {
            var stringContent = new StringContent(body, Encoding.UTF8, ContentType.JSON);

            Result response = await CreateRequest(() => PutAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<Result> Post(string requestUrl, string body, string contentType = null)
        {
            var stringContent = new StringContent(body, Encoding.UTF8, contentType ?? ContentType.JSON);

            Result response = await CreateRequest(() => PostAsync(requestUrl, stringContent));

            return response;
        }

        public async Task<Result> Get(string requestUrl)
        {
            Result response = await CreateRequest(() => GetAsync(requestUrl));

            return response;
        }

        private async Task CheckCredentials()
        {
            var expiration = credentials.ExpirationDate?.AddMinutes(-5);

            var validCredentials = DateTime.Now < credentials.ExpirationDate?.AddMinutes(-5);

            if (!validCredentials)
            {
                validCredentials = await RefreshCredentials();
            }

            if (validCredentials)
            {
                DefaultRequestHeaders.Authorization = credentials.AuthorizationHeader;
            }
        }

        private async Task<bool> RefreshCredentials()
        {
            bool validCredentials = false;
            var requestUrl = Constants.REFRESH_TOKEN_CALL;

            var request = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("refresh_token", credentials.RefreshToken),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", Constants.APP_CLIENT_ID)
            };

            var body = await new FormUrlEncodedContent(request).ReadAsStringAsync();

            var requestBody = new StringContent(body, Encoding.UTF8, ContentType.WWW_FORM);

            var response = await PostAsync(requestUrl, requestBody);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                credentials.SignIn(JToken.Parse(jsonResponse));
                validCredentials = true;
            }
            else
            {
                credentials.SignOut();
            }

            return validCredentials;
        }

        private async Task<Result> CreateRequest(Func<Task<HttpResponseMessage>> httpCall)
        {
            if (credentials?.HasSession == true)
            {
                await CheckCredentials();
            }

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
