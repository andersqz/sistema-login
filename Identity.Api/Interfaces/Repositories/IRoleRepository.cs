using Identity.Api.Models;

namespace Identity.Api.Interfaces.Repositories
{
    public interface IRoleRepository
    {
            Task<int> Insert(Role role);
            Task<bool> ExistsByName(string name);
            Task<IEnumerable<Role>> SelectAll();
            Task<Role?> SelectById(int id);
    }
}