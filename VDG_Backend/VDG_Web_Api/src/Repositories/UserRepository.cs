using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VdgDbDemoContext _context;


        public UserRepository(VdgDbDemoContext context)
        {
            _context = context;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User is not found");
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Unable to delete user,{e.Message}", e);
            }
        }

        public async Task<User?> GetById(int userId)
        {
            User? user = await _context.Users.Include(p => p.Person)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(int page, int limit)
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Person)
                    .Skip((page - 1) * limit) // Apply pagination
                    .Take(limit)
                    .ToListAsync();
                return users;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to add user, Error {ex.Message}", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to update user, Error {ex.Message}", ex);
            }
        }

        public async Task<User?> GetByEmail(string email)
        {
            try
            {
                return await _context.Users.Include(u => u.Person)
                    .FirstOrDefaultAsync(u => email == u.Email);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task UpdateUserImageAsync(int userId, string imageUrl)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    throw new Exception();

                user.ImageUrl = imageUrl;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to update user's image, Error {ex.Message}", ex);
            }
        }
        public async Task<string?> GetUserImageAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                return user.ImageUrl;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to return user's image, Error {ex.Message}", ex);
            }
        }
    }
}
