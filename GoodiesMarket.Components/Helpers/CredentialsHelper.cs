using GoodiesMarket.Components.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace GoodiesMarket.Components.Helpers
{
    public class CredentialsHelper : ICredentials
    {
        private static CredentialsHelper instance;
        public static CredentialsHelper Instance
        {
            get { return instance ?? (instance = new CredentialsHelper()); }
        }

        private CredentialsHelper() { }

        private JToken credentials;

        public void RegisterSignIn(JToken credentials)
        {
            this.credentials = credentials;
        }

        public AuthenticationHeaderValue AuthorizationHeader
        {
            get
            {
                return credentials != null ? new AuthenticationHeaderValue(TokenType, AccessToken) : null;
            }
        }

        public string RefreshToken
        {
            get
            {
                return credentials != null ? $"{credentials["refresh_token"]}" : null;
            }
        }

        public string AccessToken
        {
            get
            {
                return credentials != null ? $"{credentials["access_token"]}" : null;
            }
        }

        public string TokenType
        {
            get
            {
                return credentials != null ? $"{credentials["token_type"]}" : null;
            }
        }

        public string ClientId
        {
            get
            {
                return credentials != null ? $"{credentials["client_id"]}" : null;
            }
        }

        public DateTime? ExpirationDate
        {
            get
            {
                return credentials?.Value<DateTime?>(".expires");
            }
        }

        public DateTime? IssuedDate
        {
            get
            {
                return credentials?.Value<DateTime>(".issued");
            }
        }

        public bool HasSession
        {
            get
            {
                return credentials != null;
            }
        }

        public void LogOut()
        {
            credentials = null;
        }
    }
}
