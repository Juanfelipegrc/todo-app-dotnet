using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces.IRepositories;
using TodoApp.Application.Interfaces.IServices;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        public AuthService(IUserService userService, IPasswordHasher passwordHasher) 
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        
        }

        public async Task<UserLoginResultDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null || !_passwordHasher.Verify(request.Password, user.Password)) 
            {
                return new UserLoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "Incorrect email or password"
                };
            }

            return new UserLoginResultDTO
            {
                Success = true,
                UserId = user.Id,
                Email = user.Email
            };
        }


        public async Task<UserLoginResultDTO> RegisterAsync(RegisterRequestDTO request)
        {

            var existingUser = await _userService.GetUserByEmailAsync(request.Email);

            if(existingUser != null)
            {
                return new UserLoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "User already exists"
                };
            }
            
            try
            {

                await _userService.CreateUserAsync(request);

                var user = await _userService.GetUserByEmailAsync(request.Email);

                return new UserLoginResultDTO
                {
                    Success = true,
                    UserId = user.Id,
                    Email = user.Email
                };
            }
            catch (Exception ex) 
            {
                return new UserLoginResultDTO
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                };
            }
        }




    }
}
