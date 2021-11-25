using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Models;

namespace ToDo.Request
{
    public class TaskItemRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }

        public CategorySelector Category { get; set; }
        public PrioritySelector Priority { get; set; }
        public StatusEnum Status { get; set; }

        public int UserId { get; set; }
    }
}
