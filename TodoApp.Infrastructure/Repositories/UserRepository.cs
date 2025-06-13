using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces.IRepositories;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAppDBContext _context;
        public UserRepository(TodoAppDBContext context) 
        {
            _context = context;
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
