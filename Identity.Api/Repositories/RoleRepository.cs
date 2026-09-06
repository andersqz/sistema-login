using Identity.Api.Data;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Interfaces.Repositories;

namespace Identity.Api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
            => _context = context;

        public async Task<bool> ExistsByName(string name)
        {
            return await _context
                .Roles
                .AsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task<int> Insert(Role role)
        {
            _context
                .Roles.Add(role);
                
            await _context
                .SaveChangesAsync();

            return role.Id;
        }

        public async Task<IEnumerable<Role>> SelectAll()
        {
            return await _context
                .Roles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Role?> SelectById(int id)
        {
            return await _context
                .Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}