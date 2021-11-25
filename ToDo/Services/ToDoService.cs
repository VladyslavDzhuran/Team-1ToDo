using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Request;

namespace ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly ApplicationContext _context;

        public ToDoService (ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            IEnumerable<TaskItem> task = await _context.Tasks.ToListAsync();

            return task;
        }
        public async Task<TaskItem> GetTaskItemAsync (int TaskId)
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            return task;
        }
        public async Task CreateTaskItemAsync(TaskItemRequest taskItemRequest)
        {
            var taskItem = new TaskItem() {
                Name = taskItemRequest.Name,
                Description = taskItemRequest.Description,
                DeadLine = taskItemRequest.DeadLine,
                Category = taskItemRequest.Category,
                Status = taskItemRequest.Status,
                Priority = taskItemRequest.Priority,
                UserId = taskItemRequest.UserId
            };
            await _context.Tasks.AddAsync(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskItemAsync(int TaskId, TaskItemRequest taskItemRequest)
        {
            var task = await GetTaskItemAsync(TaskId);
            task.Name = taskItemRequest.Name;
            task.Description = taskItemRequest.Description;
            task.DeadLine = taskItemRequest.DeadLine;
            task.UserId = taskItemRequest.UserId;
            task.Status = taskItemRequest.Status;
            task.Category = taskItemRequest.Category;
            task.Description = taskItemRequest.Description;
            task.DeadLine = taskItemRequest.DeadLine;
            task.Priority = taskItemRequest.Priority;
            _context.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteTaskItemAsync(int TaskId)
        {
            var task = await GetTaskItemAsync(TaskId);
            if (task == null)
            {
                throw new NullReferenceException();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
