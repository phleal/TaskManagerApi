using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Models;

namespace TaskManagerApiTests.Utils
{

    public static class FakeDataGenerator
    {
        public static TaskItem GenerateTask(
            string? id = null,
            string title = "Título padrão",
            string description = "Descrição padrão",
            DateTime? createdAt = null,
            DateTime? dueDate = null,
            string? userId = null,
            bool isCompleted = false)
        {
            return new TaskItem
            {
                Id = id ?? Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                CreatedAt = createdAt ?? DateTime.UtcNow,
                DueDate = dueDate ?? DateTime.UtcNow.AddDays(3),
                UserId = userId ?? Guid.NewGuid().ToString(),
                IsCompleted = isCompleted
            };
        }

        public static List<TaskItem> GenerateTaskList(int count, string? userId = null)
        {
            var tasks = new List<TaskItem>();
            for (int i = 0; i < count; i++)
            {
                tasks.Add(GenerateTask(userId: userId));
            }
            return tasks;
        }
    }
}
