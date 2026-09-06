using Identity.Api.Models;

namespace Identity.Api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> Insert(User user);
        Task<IEnumerable<User>> SelectAll();
        Task<User?> SelectById(int id);
        Task<User?> SelectByEmail(string email);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<bool> ExistsByEmail(string email);
    }
}