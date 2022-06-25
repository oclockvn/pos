using Microsoft.EntityFrameworkCore;
using pos.common.extensions;
using pos.core.Data;
using pos.users.Models;

namespace pos.users.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Check if given user is valid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> IsValidUserAccountAsync(UserLogin user);

        /// <summary>
        /// Get user token info
        /// </summary>
        /// <param name="username"><see cref="UserToken"/></param>
        /// <returns></returns>
        Task<UserToken> GetUserInfoAsync(string username);
    }

    public class UserService : IUserService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public UserService(
            ITenantDbContextFactory tenantDbContextFactory)
        {
            _tenantDbContextFactory = tenantDbContextFactory;
        }

        public async Task<UserToken> GetUserInfoAsync(string username)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            return await context.Users
                .Where(x => x.Username == username)
                .Select(x => new UserToken
                {
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsValidUserAccountAsync(UserLogin user)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();

            var local = false;

#if DEBUG
            local = true;
#endif

            var hashed = user.Password.Hash();
            var filter = local ? context.Users.Where(x => x.Username == user.Username)
            : context.Users.Where(x => x.Username == user.Username && x.Password == hashed);

            var valid = await filter
                .AnyAsync();

            return valid;
        }
    }
}
