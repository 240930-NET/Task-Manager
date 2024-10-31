using TaskManager.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.API.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetAllTasks();
        Task<TaskModel> GetTaskById(int id);  // Add this method to fetch a task by ID
        Task AddTask(TaskModel task);
        Task DeleteTask(int id);
        Task UpdateTask(TaskModel task);
    }
}
