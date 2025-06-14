using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterJson()
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
                return new HttpStatusCodeResult(403, "Invalid Token");
            }

            Request.InputStream.Position = 0;
            using (var reader = new StreamReader(Request.InputStream))
            {
                var body = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<RegisterViewModel>(body);

                var result = await _authService.RegisterAsync(new RegisterRequestDTO
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                });

                if (!result.Success)
                {
                    return Json(new { success = false, error = result.ErrorMessage });

                }

                Session["UserId"] = result.UserId;
                Session["Email"] = result.Email;

                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Todo", new { userId = result.UserId })
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> LoginJson()
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
                var model = JsonConvert.DeserializeObject<LoginViewModel>(body);

                var result = await _authService.LoginAsync(new LoginRequestDTO
                {
                    Email = model.Email,
                    Password = model.Password
                });

                if (!result.Success)
                {
                    return Json(new { success = false, error = result.ErrorMessage });
                }


                Session["UserId"] = result.UserId;
                Session["Email"] = result.Email;

                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Todo", new { userId = result.UserId })
                });
            }
        }

        
        

    }
}