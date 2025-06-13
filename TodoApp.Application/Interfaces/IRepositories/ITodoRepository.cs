using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces.IRepositories
{
    public interface ITodoRepository
    {
        Task<Todo> GetTodoByIdAsync(int todoId);
        Task<IEnumerable<Todo>> GetAllTodosAsync(int userId);
        Task CreateTodoAsync (Todo todo);
    }
}
