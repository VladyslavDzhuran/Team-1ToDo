using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Request;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        public TaskItemController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("{TaskId:int}")]
        public async Task ReadTaskItem(int TaskId)
        {
            await _toDoService.GetTaskItemAsync(TaskId);
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetTasks()
        {
            var tasks = await _toDoService.GetTasksAsync();
            return tasks;
        }

        [HttpPut("{TaskId:int}")]
        public async Task UpdateTaskItem(int TaskId, TaskItemRequest taskItemRequest)
        {
            await _toDoService.UpdateTaskItemAsync(TaskId, taskItemRequest);
        }

        [HttpDelete("{TaskId:int}")]
        public async Task DeleteTaskItem (int TaskId)
        {
            await _toDoService.DeleteTaskItemAsync(TaskId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem(TaskItemRequest taskItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           await _toDoService.CreateTaskItemAsync(taskItemRequest);
           return Ok();
        }
    }
}
