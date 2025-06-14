using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces.IServices
{
    public interface ITodoService
    {
        Task<Todo> GetTodoByIdAsync(int todoId);
        Task<IEnumerable<Todo>> GetAllTodosAsync(int userId);
        Task<CreationTodoResultDTO> CreateTodoAsync(Todo todo);
        Task<CreationTodoResultDTO> UpdateStatusTodoAsync(int todoId, string newStatus);
        Task<CreationTodoResultDTO> DeleteTodoAsync(int todoId);
    }
}