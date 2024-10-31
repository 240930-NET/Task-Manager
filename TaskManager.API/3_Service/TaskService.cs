using TaskManager.API.Model;
using TaskManager.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.API.Service
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            return await _taskRepository.GetAllTasks();
        }

        public async Task AddTask(TaskModel task)
        {
            await _taskRepository.AddTask(task);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
                return false;

            await _taskRepository.DeleteTask(id);
            return true;
        }

        public async Task<bool> MarkTaskAsComplete(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            if (task == null)
                return false;

            task.IsComplete = true;
            await _taskRepository.UpdateTask(task);
            return true;
        }
    }
}
