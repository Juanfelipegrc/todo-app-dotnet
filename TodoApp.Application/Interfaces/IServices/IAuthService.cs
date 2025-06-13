using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<UserLoginResultDTO> LoginAsync(LoginRequestDTO request);
        Task<UserLoginResultDTO> RegisterAsync(RegisterRequestDTO request);
    }
}
