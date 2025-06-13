using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs
{
    public class UserLoginResultDTO
    {
        public bool Success { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string ErrorMessage { get; set; }
    }
}
