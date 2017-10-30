using GoodiesMarket.Security.API.Helpers;
using GoodiesMarket.Security.Data.Repository;
using GoodiesMarket.Security.Model;
using Microsoft.Owin.Security.Infrastructure;
using Ninject;
using System;
using System.Threading.Tasks;

namespace GoodiesMarket.Security.API.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private IKernel kernel;

        public RefreshTokenProvider(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var repository = kernel.Get<ISecurityRepository>();

            var refreshTokenLifeTime = context.OwinContext.Get<string>("clientRefreshTokenLifeTime");

            var token = new RefreshToken()
            {
                Id = HashingHelper.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = await repository.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = HashingHelper.GetHash(context.Token);

            var repository = kernel.Get<ISecurityRepository>();
            var refreshToken = await repository.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await repository.RemoveRefreshToken(hashedTokenId);
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }
    }

}