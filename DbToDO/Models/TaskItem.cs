using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class TaskItem
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public CategorySelector Category { get; set; }
        public PrioritySelector Priority { get; set; }
        public StatusEnum Status { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}