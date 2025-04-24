using TaskManagerApi.Models;

namespace TaskManagerApi.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task CreateAsync(TaskItem task);
        Task<List<TaskItem>> GetByUserIdAsync(string userId);
        Task<TaskItem?> GetByIdAsync(string id);
        Task DeleteAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
    }
}
