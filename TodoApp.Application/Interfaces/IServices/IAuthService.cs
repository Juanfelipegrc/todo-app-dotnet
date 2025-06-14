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
        Task<AuthResultDTO> LoginAsync(LoginRequestDTO request);
        Task<AuthResultDTO> RegisterAsync(RegisterRequestDTO request);
    }
}
