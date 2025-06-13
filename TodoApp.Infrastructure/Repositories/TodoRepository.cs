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
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDBContext _context;

        public TodoRepository(TodoAppDBContext context)
        {
            _context = context;
        }

        public async Task<Todo> GetTodoByIdAsync(int todoId)
        {
            var todo = await _context.Todos.FindAsync(todoId);

            if (todo == null)
            {
                throw new Exception("Todo not found");
            }

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync(int userId)
        {
            var todos = await _context.Todos
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return todos;
        }

        public async Task CreateTodoAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }
    }
}
