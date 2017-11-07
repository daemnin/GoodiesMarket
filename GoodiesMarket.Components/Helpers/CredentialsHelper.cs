using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace GoodiesMarket.Components.Helpers
{
    public static class CredentialsHelper
    {
        private static JToken credentials;

        public static void RegisterSignIn(JToken credentials)
        {
            CredentialsHelper.credentials = credentials;
        }

        public static KeyValuePair<string, string> AuthHeader
        {
            get
            {
                return new KeyValuePair<string, string>("Authorization", $"Bearer {credentials["access_token"]}");
            }
        }

        public static string RefreshToken
        {
            get
            {
                return $"{credentials["refresh_token"]}";
            }
        }

        public static string AccessToken
        {
            get
            {
                return $"{credentials["access_token"]}";
            }
        }

        public static string ClientId
        {
            get
            {
                return $"{credentials["client_id"]}";
            }
        }

        public static DateTime ExpirationDate
        {
            get
            {
                return credentials.Value<DateTime>(".expires");
            }
        }

        public static DateTime IssuedDate
        {
            get
            {
                return credentials.Value<DateTime>(".issued");
            }
        }
    }
}
