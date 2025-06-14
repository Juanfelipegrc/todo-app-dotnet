using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    public class CreateTodoViewModel
    {

        [Required(ErrorMessage = "Name is obligaroty")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Status is obligatory")]
        public string Status { get; set; }
    }
}