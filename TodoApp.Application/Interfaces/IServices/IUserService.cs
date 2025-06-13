using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task CreateUserAsync(RegisterRequestDTO request);
        Task<User> GetUserByEmailAsync(string email);
    }
}
