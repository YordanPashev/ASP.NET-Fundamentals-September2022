namespace TaskBoardApp.Services.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using TaskBoardApp.Data;

    [TestFixture]
    public class Tests
    {
        [Test]
        public async System.Threading.Tasks.Task Test_CreateNewBoardAsync_Must_Return_True()
        {
            var options = new DbContextOptionsBuilder<TaskBoardDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var dbContext = new TaskBoardDbContext(options, true);

            BoardsService boardsService = new BoardsService(dbContext);
            string boardName = ("TODO");
            int expectedBoardCount = 1;

            await boardsService.CreateNewBoardAsync(boardName);
            int realBoardCount = await dbContext.Boards.CountAsync();
            Assert.That(realBoardCount == expectedBoardCount, "The count of all boards must be 1."); 
        }
    }
}