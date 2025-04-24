using TaskManagerApi.Models;

namespace TaskManagerApi.Interfaces.Services
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskItem task);
        Task<List<TaskItem>> GetTasksByUserAsync(string userId);
        Task<TaskItem> GetTaskByIdAsync(string id);
        Task<bool> CompleteTaskAsync(string id);
        Task<bool> DeleteTaskAsync(string id);
    }
}
