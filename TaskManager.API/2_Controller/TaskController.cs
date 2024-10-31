using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Model;
using TaskManager.API.Service;
using System.Threading.Tasks;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // Get all tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

        // Add a new task
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskService.AddTask(task);
            return Ok();
        }

        // Delete a task by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.DeleteTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok();
        }

        // Mark a task as complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkTaskAsComplete(int id)
        {
            var task = await _taskService.MarkTaskAsComplete(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
