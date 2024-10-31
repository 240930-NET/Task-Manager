using Moq;
using TaskManager.API.Model;
using TaskManager.API.Repository;
using TaskManager.API.Service;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _taskService = new TaskService(_taskRepositoryMock.Object);
    }

    // Test for Adding a Task
    [Fact]
    public async Task AddTask_CallsRepositoryOnce()
    {
        // Arrange
        var newTask = new TaskModel { Name = "Test Task", Description = "Description" };

        // Act
        await _taskService.AddTask(newTask);

        // Assert
        _taskRepositoryMock.Verify(repo => repo.AddTask(newTask), Times.Once);
    }

    // Test for Retrieving All Tasks
    [Fact]
    public async Task GetTasks_ReturnsAllTasks()
    {
        // Arrange
        var taskList = new List<TaskModel>
        {
            new TaskModel { Id = 1, Name = "Test Task 1", IsComplete = false },
            new TaskModel { Id = 2, Name = "Test Task 2", IsComplete = true }
        };

        _taskRepositoryMock.Setup(repo => repo.GetAllTasks()).ReturnsAsync(taskList);

        // Act
        var result = await _taskService.GetTasks();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Test Task 1", result.First().Name);
        _taskRepositoryMock.Verify(repo => repo.GetAllTasks(), Times.Once);
    }

    // Test for Deleting a Task by ID
    [Fact]
    public async Task DeleteTask_ReturnsTrue_WhenTaskExists()
    {
        // Arrange
        var task = new TaskModel { Id = 1, Name = "Test Task", IsComplete = false };
        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1)).ReturnsAsync(task);

        // Act
        var result = await _taskService.DeleteTask(1);

        // Assert
        Assert.True(result);
        _taskRepositoryMock.Verify(repo => repo.DeleteTask(1), Times.Once);
    }

    // Test for Marking a Task as Complete
    [Fact]
    public async Task MarkTaskAsComplete_UpdatesTask_WhenTaskExists()
    {
        // Arrange
        var task = new TaskModel { Id = 1, Name = "Test Task", IsComplete = false };
        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1)).ReturnsAsync(task);

        // Act
        var result = await _taskService.MarkTaskAsComplete(1);

        // Assert
        Assert.True(result);
        Assert.True(task.IsComplete);
        _taskRepositoryMock.Verify(repo => repo.UpdateTask(task), Times.Once);
    }

    // Test for Task Not Found when Deleting
    [Fact]
    public async Task DeleteTask_ReturnsFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        _taskRepositoryMock.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync((TaskModel)null);

        // Act
        var result = await _taskService.DeleteTask(1);

        // Assert
        Assert.False(result);
        _taskRepositoryMock.Verify(repo => repo.DeleteTask(It.IsAny<int>()), Times.Never);
    }
}
