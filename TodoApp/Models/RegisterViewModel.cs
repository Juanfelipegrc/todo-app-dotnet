using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is obligatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is obligatory")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is obligatory")]
        public string Password { get; set; }
    }
}