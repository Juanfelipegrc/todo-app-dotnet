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
using System.IO;
using Newtonsoft.Json;
using TodoApp.Models;

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
        public async Task<ActionResult> Index()
        {

            if(Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int userId = (int)Session["UserId"];

            var todos = await _todoService.GetAllTodosAsync(userId);

            return View(todos);
        }

        // GET: Todo by id

        public async Task<ActionResult> Search(int todoId)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var todo = await _todoService.GetTodoByIdAsync(todoId);

            return View(todo);
        }
        
        // GET: Todo/Create
        public async Task<ActionResult> Create()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            return View();
        }

        // GET: Todo/Update
        public async Task<ActionResult> Update(int todoId)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var todo = await _todoService.GetTodoByIdAsync(todoId);
            return View(todo);
        }

        [HttpPost]
        public async Task<ActionResult> CreateJson()
        {
            try
            {
                var tokenHeader = Request.Headers["RequestVerificationToken"];
                var tokens = tokenHeader.Split(':');
                var cookieToken = tokens[0].Trim();
                var formToken = tokens[1].Trim();

                System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);
            }
            catch
            {
                return new HttpStatusCodeResult(403, "Invalid Code");
            }

            Request.InputStream.Position = 0;
            using (var reader = new StreamReader(Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<CreateTodoViewModel>(body);
                var userId = (int)Session["UserId"];
                var result = await _todoService.CreateTodoAsync(new Todo
                {
                    Name = model.Name,
                    Status = model.Status,
                    UserId = userId
                });

                if (!result.Success)
                {
                    return Json(new { success = false, error = result.ErrorMessage });
                }


                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Todo")
                });



            }

        }


        [HttpPost]
        public async Task<ActionResult> DeleteJson()
        {
            try
            {
                var tokenHeader = Request.Headers["RequestVerificationToken"];
                var tokens = tokenHeader.Split(':');
                var cookieToken = tokens[0].Trim();
                var formToken = tokens[1].Trim();

                System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);
            }
            catch
            {
                return new HttpStatusCodeResult(403, "Invalid Code");
            }

            Request.InputStream.Position = 0;
            using (var reader = new StreamReader(Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<dynamic>(body);
                var userId = (int)Session["UserId"];
                var result = await _todoService.DeleteTodoAsync((int)model.TodoId);

                if (!result.Success)
                {
                    return Json(new { success = false, error = result.ErrorMessage });
                }


                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Todo")
                });



            }

        }


        [HttpPost]
        public async Task<ActionResult> UpdateJson()
        {
            try
            {
                var tokenHeader = Request.Headers["RequestVerificationToken"];
                var tokens = tokenHeader.Split(':');
                var cookieToken = tokens[0].Trim();
                var formToken = tokens[1].Trim();

                System.Web.Helpers.AntiForgery.Validate(cookieToken, formToken);
            }
            catch
            {
                return new HttpStatusCodeResult(403, "Invalid Code");
            }

            Request.InputStream.Position = 0;
            using (var reader = new StreamReader(Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<dynamic>(body);
                var userId = (int)Session["UserId"];
                var result = await _todoService.UpdateStatusTodoAsync((int)model.TodoId, (string)model.Status);

                if (!result.Success)
                {
                    return Json(new { success = false, error = result.ErrorMessage });
                }


                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Todo")
                });



            }

        }

    }
}
