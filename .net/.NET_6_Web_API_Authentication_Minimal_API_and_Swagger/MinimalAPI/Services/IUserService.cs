using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface IUserService
    {
        public User GetUser(UserLogin userLogin);
    }
}
