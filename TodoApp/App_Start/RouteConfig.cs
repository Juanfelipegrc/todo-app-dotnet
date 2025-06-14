using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TodoApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "Auth/Register",
                defaults: new { controller = "Auth", action = "Register" }
            );

            routes.MapRoute(
                name: "GetAllTodos",
                url: "Todo/All",
                defaults: new { controller = "Todo", action = "Index"}
            );

            routes.MapRoute(
                name: "CreateTodo",
                url: "Todo/Create",
                defaults: new { controller = "Todo", action = "Create" }
            );

            routes.MapRoute(
                name: "UpdateTodo",
                url: "Todo/Update",
                defaults: new { controller = "Todo", action = "Update" }
            );

            routes.MapRoute(
                name: "GetTodoById",
                url: "Todo/Search/{todoId}",
                defaults: new { controller = "Todo", action = "Search"}
            );
        }
    }
}
