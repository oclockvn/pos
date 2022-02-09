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
        Task<UserToken> GetUserTokenInfoAsync(string username);
    }

    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public Task<UserToken> GetUserTokenInfoAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidUserAccountAsync(UserLogin user)
        {
            throw new NotImplementedException();
        }
    }
}
