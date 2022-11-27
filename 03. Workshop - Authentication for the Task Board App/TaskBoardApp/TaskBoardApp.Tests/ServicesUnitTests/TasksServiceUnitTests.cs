namespace TaskBoardAppTests.ServicesUnitTests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using TaskBoardApp.Data;
    using TaskBoardApp.Data.Entities;
    using TaskBoardApp.Models;
    using TaskBoardApp.Services;
    using TaskBoardAppTests.Common;

    [TestFixture]
    public class TasksServiceUnitTests
    {
        private TasksService tasksService;
        private BoardsService boardsService;
        private TaskBoardDbContext dbContext;
        private Board board;
        private User user;


        [SetUp]
        public void SetUp()
        {
            DbContextOptions<TaskBoardDbContext> options = new DbContextOptionsBuilder<TaskBoardDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            this.dbContext = new TaskBoardDbContext(options, true);
            this.boardsService = new BoardsService(this.dbContext);
            this.tasksService = new TasksService(this.dbContext, this.boardsService);
        }

        [Test]
        public async System.Threading.Tasks.Task Test_CreateNewTaskAsync_Must_Return_True()
        {
            this.user = await HelperMethods.CreateUserWithNameKichka(this.dbContext);
            string boardName = "TODO";
            this.board = HelperMethods.CreateBoard(boardName);
            this.dbContext.Boards.Add(this.board);
            int expectedTasksCount = 1;
            string title = "Test";
            string description = "This is a test title for taskboardService.";
            Guid boardId = this.board.Id;
            string ownerUserName = this.user.UserName;
            CreateTaskViewModel model = HelperMethods.CreateNewTaskViewModel(title, description, boardId, ownerUserName);
            
            await this.tasksService.CreateNewTaskAsync(model);
            int realTaskCount = await dbContext.Tasks.CountAsync();
            Task? resultTask = await this.dbContext.Tasks.FirstOrDefaultAsync(t => t.Title == title);

            Assert.IsNotNull(resultTask, "The task shouldn't be null.");
            Assert.That(expectedTasksCount == realTaskCount, $"Tasks Count in DB must be {expectedTasksCount}.");
            Assert.That(resultTask?.Title == title && resultTask.Description == description &&
                        resultTask.BoardId == boardId && resultTask.Owner.UserName == ownerUserName,
                        $"The task data is wrong. It must have following data - Title: {title}, Description: {description}, BoardID: {boardId}, OWnerID: {ownerUserName}.");
        }
    }
}