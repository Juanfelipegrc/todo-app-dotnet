using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces.IRepositories;
using TodoApp.Application.Interfaces.IServices;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) 
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return user;
        }

        public async Task CreateUserAsync(RegisterRequestDTO request)
        {

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _passwordHasher.Hash(request.Password)
            };

            await _userRepository.CreateUserAsync(user);

        }


    }
}
