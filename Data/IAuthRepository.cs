using System.Threading.Tasks;
using Models;

namespace Data
{
    public interface IAuthRepository
    {
        // always start interface name with capital I

        // doesn't contain implementation code. serves as a contract to actual (concrete) repository that says you must support these methods. implementation code is then written in the concrete repo.

        // interface only contains the signatures of the methods

        // Register User
        Task<User> Register(User user, string password);

        // Login User to API
        Task<User> Login(string username, string password);

        // Check if User exists (unique user names)
        Task<bool> UserExists(string username);

    }
}