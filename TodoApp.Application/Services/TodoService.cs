using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces.IRepositories;
using TodoApp.Application.Interfaces.IServices;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        public async Task<Todo> GetTodoByIdAsync(int todoId)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(todoId);

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync(int userId)
        {
            var todos = await _todoRepository.GetAllTodosAsync(userId);

            return todos;
        }

        public async Task<CreationTodoResultDTO> CreateTodoAsync(Todo todo)
        {

            try
            {
                await _todoRepository.CreateTodoAsync(todo);

                return new CreationTodoResultDTO
                {
                    Success = true,
                };
            }
            catch 
            {

                return new CreationTodoResultDTO
                {
                    Success = false,
                    ErrorMessage = "Error Creating Todo"
                };

                
            }

        }

        public async Task<CreationTodoResultDTO> UpdateStatusTodoAsync(int todoId, string newStatus)
        {
            try
            {
                await _todoRepository.UpdateStatusTodoAsync(todoId, newStatus);

                return new CreationTodoResultDTO
                {
                    Success = true,
                };
            }
            catch
            {
                return new CreationTodoResultDTO
                {
                    Success = false,
                    ErrorMessage = "Error Updating Todo"
                };
            }
        }


        public async Task<CreationTodoResultDTO> DeleteTodoAsync(int todoId)
        {
            try
            {
                await _todoRepository.DeleteTodoAsync(todoId);

                return new CreationTodoResultDTO
                {
                    Success = true,
                };
            }
            catch
            {
                return new CreationTodoResultDTO
                {
                    Success = false,
                    ErrorMessage = "Error Deleting Todo"
                };
            }
        }
    }
}
