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
    }

    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public Task<bool> IsValidUserAccountAsync(UserLogin user)
        {
            throw new NotImplementedException();
        }
    }
}
