using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces.IServices;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // GET: Auth

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid) 
            { 
                return View(model);
            }

            var result = await _authService.RegisterAsync(new RegisterRequestDTO
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            });

            if (!result.Success)
            {
                ViewBag.Error = result.ErrorMessage;
                return View(model);
            }

            Session["UserId"] = result.UserId;
            Session["Email"] = result.Email;

            return RedirectToAction("Index", "Todo", new {userId = result.UserId});


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _authService.LoginAsync(new LoginRequestDTO
            {
                Email = model.Email,
                Password = model.Password
            });

            if (!result.Success) 
            {
                ViewBag.Error = result.ErrorMessage;
                return View(model);
            }

            
            Session["UserId"] = result.UserId;
            Session["Email"] = result.Email;

            return RedirectToAction("Index", "Todo", new {userId = result.UserId});
        }

    }
}