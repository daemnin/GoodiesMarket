﻿using GoodiesMarket.Components.Configs;
using GoodiesMarket.Components.Helpers;
using GoodiesMarket.Components.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodiesMarket.Components.Proxies
{
    public class AccountProxy : ProxyBase
    {
        private readonly string Controller = "Account";

        public async Task<Result> SignIn(string email, string password)
        {
            var requestUri = $"{Controller}/Login";

            var request = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", Constants.APP_CLIENT_ID)
            };

            var body = await new FormUrlEncodedContent(request).ReadAsStringAsync();

            using (var httpClient = GetSecureClient())
            {
                var result = await httpClient.Post(requestUri, body, ContentType.WWW_FORM);

                return result;
            }
        }

        public async Task<Result> Register(string name, string email, string password, bool isSeller)
        {
            var requestUri = $"{Controller}/Register";

            var request = new
            {
                name = name,
                email = email,
                password = password,
                roleType = isSeller ? 1 : 0
            };

            var body = JsonConvert.SerializeObject(request);

            using (var httpClient = GetClient())
            {
                var result = await httpClient.Post(requestUri, body);

                return result;
            }
        }

        public async Task<Result> UpdateProfile(string motto = null, int? range = null, double? latitude = null, double? longitude = null)
        {
            var requestUri = $"{Controller}";

            var request = new
            {
                latitude,
                longitude,
                motto,
                range
            };

            var body = JsonConvert.SerializeObject(request);

            using (var client = GetClient(Credentials.Instance))
            {
                var result = await client.Put(requestUri, body);

                return result;
            }
        }

        public async Task<Result> GetRole()
        {
            var requestUri = $"{Controller}/role";

            using (var httpClient = GetClient(Credentials.Instance))
            {
                var result = await httpClient.Get(requestUri);

                return result;
            }
        }

        public async Task<Result> GetProfile()
        {
            var requestUri = $"{Controller}";

            using (var httpClient = GetClient(Credentials.Instance))
            {
                var result = await httpClient.Get(requestUri);

                return result;
            }
        }
    }
}