using MinimalAPI.Models;
using MinimalAPI.Repositories;

namespace MinimalAPI.Services
{
    public class UserService : IUserService
    {
        public User GetUser(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals
                (userLogin.Username, StringComparison.OrdinalIgnoreCase) &&
                o.Password.Equals(userLogin.Password));

            return user;
        }
    }
}
