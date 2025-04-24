using Moq;
using TaskManagerApi.Interfaces.Repositories;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApiTests.Utils;

namespace TaskManagerApi.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_Should_Call_Repository()
        {
            var task = FakeDataGenerator.GenerateTask();

            await _taskService.CreateTaskAsync(task);

            _taskRepositoryMock.Verify(r => r.CreateAsync(task), Times.Once);
        }

        [Fact]
        public async Task GetTasksByUserAsync_Should_Return_Tasks()
        {
            var userId = "user123";
            var fakeTasks = FakeDataGenerator.GenerateTaskList(3);
            _taskRepositoryMock.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(fakeTasks);

            var result = await _taskService.GetTasksByUserAsync(userId);

            Assert.Equal(3, result.Count);
            _taskRepositoryMock.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetTaskByIdAsync_Should_Return_Task()
        {
            var task = FakeDataGenerator.GenerateTask();
            _taskRepositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await _taskService.GetTaskByIdAsync(task.Id);

            Assert.Equal(task, result);
        }

        [Fact]
        public async Task CompleteTaskAsync_Should_Return_True_When_Successful()
        {
            var task = FakeDataGenerator.GenerateTask();
            _taskRepositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await _taskService.CompleteTaskAsync(task.Id);

            Assert.True(result);
            Assert.True(task.IsCompleted);
            _taskRepositoryMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task CompleteTaskAsync_Should_Return_False_If_Task_Not_Found()
        {
            _taskRepositoryMock.Setup(r => r.GetByIdAsync("invalid")).ReturnsAsync((TaskItem)null);

            var result = await _taskService.CompleteTaskAsync("invalid");

            Assert.False(result);
        }

        [Fact]
        public async Task CompleteTaskAsync_Should_Return_False_If_Already_Completed()
        {
            var task = FakeDataGenerator.GenerateTask();
            task.IsCompleted = true;
            _taskRepositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await _taskService.CompleteTaskAsync(task.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteTaskAsync_Should_Return_True_When_Successful()
        {
            var task = FakeDataGenerator.GenerateTask();
            _taskRepositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var result = await _taskService.DeleteTaskAsync(task.Id);

            Assert.True(result);
            _taskRepositoryMock.Verify(r => r.DeleteAsync(task), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_Should_Return_False_When_Task_Not_Found()
        {
            _taskRepositoryMock.Setup(r => r.GetByIdAsync("invalid")).ReturnsAsync((TaskItem)null);

            var result = await _taskService.DeleteTaskAsync("invalid");

            Assert.False(result);
        }
    }
}
