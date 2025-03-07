using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TodoContext _context;

        public TaskController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            return await _context.TaskItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            TaskItem? targetItem = await _context.TaskItems.FindAsync(id);
            if (targetItem == null)
                return NoContent();
            return targetItem;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItemDTO taskItemDto)
        {
            var taskItem = new TaskItem
            {
                Description = taskItemDto.Description,
                IsCompleted = taskItemDto.IsCompleted,
            };
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("description/{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItemDescriptionDTO taskItemDto)
        {
            var todoItem = await _context.TaskItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Description = taskItemDto.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("complete/{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItemCompletedDTO taskItemDto)
        {
            var todoItem = await _context.TaskItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.IsCompleted = taskItemDto.IsCompleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            TaskItem? taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
                return NotFound();
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
