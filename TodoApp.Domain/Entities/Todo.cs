﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }


        public virtual User User { get; set; }
    }
}
