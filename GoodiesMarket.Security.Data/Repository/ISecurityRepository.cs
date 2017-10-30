using GoodiesMarket.Security.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodiesMarket.Security.Data.Repository
{
    public interface ISecurityRepository : IDisposable
    {
        Task<IdentityResult> CreateUser(string username, string password);
        Task<Client> FindClient(string clientId);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        Task<IdentityUser> FindUser(string username, string password);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken token);

        Task<ICollection<RefreshToken>> GetAllRefreshTokens();
        Task<ICollection<IdentityRole>> GetRoles(IdentityUser user);
    }
}
