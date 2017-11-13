using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;

namespace GoodiesMarket.Components.Contracts
{
    public interface ICredentials
    {
        string RefreshToken { get; }
        string AccessToken { get; }
        string TokenType { get; }
        string ClientId { get; }
        bool HasSession { get; }

        AuthenticationHeaderValue AuthorizationHeader { get; }
        DateTime? ExpirationDate { get; }
        DateTime? IssuedDate { get; }

        void SignIn(JToken credentials);
        void SignOut();
    }
}
