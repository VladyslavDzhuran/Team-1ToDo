using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo;
using ToDo.Models;
using ToDo.Request;
using Xunit;

namespace Tests
{
    public class BackendTests
    {
        // Tests for CreateTaskAsync method
        [Fact]
        public async Task CreateTaskAsync_CountCheck()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            // Act
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name" });
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "anotherName" });

            // Assert
            var actual = await db.Tasks.ToListAsync();
            actual.Count.Should().Be(2);
        }

        [Fact]
        public async Task CreateTaskAsync_SouldBeNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            // Act
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = null });

            // Assert
            var actual = await db.Tasks.FirstAsync();
            actual.Name.Should().BeEquivalentTo(null);
        }
        [Fact]
        public async Task CreateTaskAsync_CorectValues()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            // Act
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name" });

            // Assert
            var actual = await db.Tasks.FirstAsync();
            actual.Name.Should().BeEquivalentTo("Name");
        }
        [Fact]
        public async Task CreateTaskAsync_TryCategoryEnum()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            // Act
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name", Category = CategorySelector.Work });

            // Assert
            var actual = await db.Tasks.FirstAsync();
            actual.Category.Should().Equals(CategorySelector.Work);
        }

        [Fact]
        public async Task CreateTaskAsync_TryEvrithing()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            var expected = new List<TaskItem> { new TaskItem { Name = "Name", Description = "desc", Category = CategorySelector.Work, Priority = PrioritySelector.Important, Status = StatusEnum.Inprogress } };
            db.Tasks.AddRange(expected);
            db.SaveChanges();

            // Act
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name", Description = "desc", Category = CategorySelector.Work, Priority = PrioritySelector.Important, Status = StatusEnum.Inprogress });

            // Assert
            var actual = await db.Tasks.FirstAsync();
            actual.Should().BeSameAs(expected);
        }
        // Tests for GetTasksAsync method
        [Fact]
        public async Task GetTasksAsync_ReturnsCorrectValues()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            var list = new List<TaskItem> { new TaskItem { Name = "Name" }, new TaskItem { Name = "anotherName" } };
            db.Tasks.AddRange(list);
            db.SaveChanges();

            // Act
            var actual = await toDoService.GetTasksAsync();

            // Assert
            actual.Should().BeEquivalentTo(list);
        }

        // Tests for GetTaskItemAsync method
        [Fact]
        public async Task GetTaskItemAsync_ReturnsCorrectValues()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            var list = new List<TaskItem> { new TaskItem { Name = "Name" }, new TaskItem { Name = "anotherName" } };
            db.Tasks.AddRange(list);
            db.SaveChanges();

            // Act
            var actual = await toDoService.GetTaskItemAsync(2);

            // Assert
            actual.Name.Should().BeEquivalentTo("anotherName");
        }
        [Fact]
        public async Task GetTaskItemAsync_ShouldBeNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);

            var list = new List<TaskItem> { new TaskItem { Name = "Name" }, new TaskItem { Name = "anotherName" } };
            db.Tasks.AddRange(list);
            db.SaveChanges();

            // Act
            var actual = await toDoService.GetTaskItemAsync(3);

            // Assert
            actual.Should().BeNull();
        }

        // Tests for UpdateTaskItemAsync method
        [Fact]
        public async Task UpdateTaskItemAsync_SouldRenameTaskItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name" });

            // Act
            await toDoService.UpdateTaskItemAsync(1, new TaskItemRequest {Name = "anotherName" });

            // Assert
            var actual = await db.Tasks.FirstAsync();
            actual.Name.Should().BeEquivalentTo("anotherName");
        }

        // Tests for DeleteTaskItemAsync method 
        [Fact]
        public async Task DeleteTaskItemAsync_SouldDeleteTaskItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DbToDo")
                .Options;
            var db = new ApplicationContext(options);
            var toDoService = new ToDoService(db);
            await toDoService.CreateTaskItemAsync(new TaskItemRequest { Name = "Name"});

            // Act
            await toDoService.DeleteTaskItemAsync(1);

            // Assert
            var actual = await db.Tasks.ToListAsync();
            actual.Count.Should().Be(0);
        }
    }
}
