using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Application.Interfaces.IServices;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todos
        public async Task<ActionResult> Index(int userId)
        {
            var todos = await _todoService.GetAllTodosAsync(userId);

            return View(todos);
        }

        // GET: Todo by id

        public async Task<ActionResult> Search(int todoId)
        {
            var todo = await _todoService.GetTodoByIdAsync(todoId);

            return View(todo);
        }
        
        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _todoService.CreateTodoAsync(todo);

                return RedirectToAction("Index", new { userId = todo.Id });
            }

            return View(todo);
        }

     
    }
}
