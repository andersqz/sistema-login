
using Identity.Api.Data;
using Identity.Api.Interfaces.Repositories;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
            => _context = context;
        public async Task<int> Insert(User user)
        {
            _context
                .Users
                .Add(user);

            await _context
                .SaveChangesAsync();
                return user.Id;
        }


        public async Task<bool> Update(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser is null)
                return false;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> Delete(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser is null)
                return false;

            _context.Users.Remove(existingUser);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> SelectAll()
        {
            return await _context
                .Users
                .Include(x => x.Roles)
                .AsNoTracking()
                .OrderByDescending(X => X.Name)
                .ToListAsync();
        }

        public async Task<User?> SelectById(int id)
        {
            return await _context
                .Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> SelectByEmail(string email)
        {
            return await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context
                .Users
                .AsNoTracking()
                .AnyAsync(x => x.Email == email);
        }
    }
}