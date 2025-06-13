using System.Web.Mvc;
using TodoApp.Application.Interfaces.IRepositories;
using TodoApp.Application.Interfaces.IServices;
using TodoApp.Application.Services;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;
using Unity;
using Unity.Mvc5;

namespace TodoApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ITodoRepository, TodoRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<ITodoService, TodoService>();
            container.RegisterType<IPasswordHasher, PasswordHasher>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IUserService, UserService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}