using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
