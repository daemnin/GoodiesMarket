using GoodiesMarket.Security.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GoodiesMarket.Security.Data.Repository
{
    public class SecurityRepository : ISecurityRepository
    {
        private GoodiesMarketAuthContext context;
        private UserManager<IdentityUser> manager;

        public SecurityRepository(GoodiesMarketAuthContext context)
        {
            this.context = context;
            manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = await context.RefreshTokens.FirstOrDefaultAsync(t => t.Subject.Equals(token.Subject) && t.ClientId.Equals(token.ClientId));

            if (existingToken != null)
            {
                await RemoveRefreshToken(existingToken);
            }

            context.RefreshTokens.Add(token);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IdentityResult> CreateUser(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = username,
                EmailConfirmed = true
            };

            var result = await manager.CreateAsync(user, password);

            return result;
        }

        public async Task<Client> FindClient(string clientId)
        {
            var client = await context.Clients.FindAsync(clientId);

            return client;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await context.RefreshTokens.FindAsync(refreshTokenId);
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            return await manager.FindAsync(username, password);
        }

        public async Task<ICollection<RefreshToken>> GetAllRefreshTokens()
        {
            return await context.RefreshTokens.ToListAsync();
        }

        public async Task<IdentityResult> AssignRole(string username, string password, string roleName)
        {
            IdentityUser user = await FindUser(username, password);

            return await manager.AddToRoleAsync(user.Id, roleName);
        }

        public async Task<ICollection<IdentityRole>> GetRoles(IdentityUser user)
        {
            var roles = new List<IdentityRole>();

            foreach (var role in user.Roles.Select(r => r.RoleId))
            {
                roles.Add(await context.Roles.FirstOrDefaultAsync(r => r.Id == role));
            }

            return roles;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken token)
        {
            context.RefreshTokens.Remove(token);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var token = await context.RefreshTokens.FindAsync(refreshTokenId);

            if (token == null) return await Task.FromResult(false);

            context.RefreshTokens.Remove(token);

            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            context.Dispose();
            manager.Dispose();
        }
    }
}
