using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace GoodiesMarket.Components.Helpers
{
    public static class CredentialsHelper
    {
        private static JToken credentials;

        public static void RegisterSignIn(JToken credentials)
        {
            CredentialsHelper.credentials = credentials;
        }

        public static AuthenticationHeaderValue AuthorizationHeader
        {
            get
            {
                return credentials != null ? new AuthenticationHeaderValue(TokenType, AccessToken) : null;
            }
        }

        public static string RefreshToken
        {
            get
            {
                return credentials != null ? $"{credentials["refresh_token"]}" : null;
            }
        }

        public static string AccessToken
        {
            get
            {
                return credentials != null ? $"{credentials["access_token"]}" : null;
            }
        }

        public static string TokenType
        {
            get
            {
                return credentials != null ? $"{credentials["token_type"]}" : null;
            }
        }

        public static string ClientId
        {
            get
            {
                return credentials != null ? $"{credentials["client_id"]}" : null;
            }
        }

        public static DateTime? ExpirationDate
        {
            get
            {
                return credentials?.Value<DateTime?>(".expires");
            }
        }

        public static DateTime? IssuedDate
        {
            get
            {
                return credentials?.Value<DateTime>(".issued");
            }
        }

        public static bool HasSession
        {
            get
            {
                return credentials != null;
            }
        }

        internal static void LogOut()
        {
            credentials = null;
        }
    }
}
