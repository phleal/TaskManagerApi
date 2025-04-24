using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Interfaces.Repositories;
using TaskManagerApi.Interfaces.Services;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            await _repository.CreateAsync(task);
        }

        public async Task<TaskItem> GetTaskByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<TaskItem>> GetTasksByUserAsync(string userId) =>
            await _repository.GetByUserIdAsync(userId);

        public async Task<bool> CompleteTaskAsync(string id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task == null || task.IsCompleted)
                return false; 
            

            task.IsCompleted = true;
            await _repository.UpdateAsync(task);
            return true;
        }


        public async Task<bool> DeleteTaskAsync(string id)
        {
            var task = await _repository.GetByIdAsync(id);

            if (task is null) 
                return false;

            await _repository.DeleteAsync(task);
            return true;
        }
    }
}
