namespace TaskBoardApp.Services.Tests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using Data;
    using Models;

    [TestFixture]
    public class BoardsServicesUnitTests
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

            Assert.That(realBoardCount == expectedBoardCount, $"The count of all boards in db must be {expectedBoardCount}."); 
        }

        [Test]
        public async System.Threading.Tasks.Task Test_GetAllBoardsAsync_Must_Return_True()
        {
            var options = new DbContextOptionsBuilder<TaskBoardDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var dbContext = new TaskBoardDbContext(options, true);
            BoardsService boardsService = new BoardsService(dbContext);

            // The boards will be ordered alphabetically therefore the names must in same order.
            string boardNameOne = "In Progress";
            string boardNameTwo = "Open";
            string boardNameThree = "TODO";
            int expectedBoardCount = 3;

            await boardsService.CreateNewBoardAsync(boardNameOne);
            await boardsService.CreateNewBoardAsync(boardNameTwo);
            await boardsService.CreateNewBoardAsync(boardNameThree);

            // The boards will be ordered alphabetically
            List<BoardViewModel> boardsResult =  await boardsService.GetAllBoardsAsync();

            int firstBoardIndex = 0;
            int secondBoardIndex = 1;
            int thirdBoardIndex = 2;

            Assert.That(boardsResult.Count == expectedBoardCount && boardsResult[firstBoardIndex].Name == boardNameOne &&
                        boardsResult[secondBoardIndex].Name == boardNameTwo && boardsResult[thirdBoardIndex].Name == boardNameThree, 
                        $"The result (count of all boards in db) be {expectedBoardCount} and the names of boards must be identical.");
        }
    }
}