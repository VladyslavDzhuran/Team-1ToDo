using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public CategorySelector Category { get; set; }
        public PrioritySelector Priority { get; set; }
        public StatusEnum Status { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}