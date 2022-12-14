namespace TaskBoardAppTests.ServicesUnitTests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using TaskBoardApp.Data;
    using TaskBoardApp.Data.Entities;
    using TaskBoardApp.Models;
    using TaskBoardApp.Models.Boards;
    using TaskBoardApp.Services;
    using TaskBoardAppTests.Common;

    [TestFixture]
    public class BoardsServiceUnitTests
    {
        private BoardsService boardsService;
        private TaskBoardDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = HelperMethods.CreateTaskBoardDbContextMock();
            this.boardsService = new BoardsService(dbContext);
        }

        [Test]
        public async System.Threading.Tasks.Task Test_CreateNewBoardAsync_Must_Return_True()
        {
            string boardName = ("TODO");
            int expectedBoardCount = 1;

            await this.boardsService.CreateNewBoardAsync(boardName);
            int realBoardCount = await dbContext.Boards.CountAsync();

            Assert.That(realBoardCount == expectedBoardCount, $"The count of all boards in db must be {expectedBoardCount}.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_GetAllBoardsAsync_Must_Return_True()
        {
            // The boards will be ordered alphabetically therefore the names must be in the same order.
            string boardNameOne = "In Progress";
            string boardNameTwo = "Open";
            string boardNameThree = "TODO";
            int expectedBoardCount = 3;

            await this.boardsService.CreateNewBoardAsync(boardNameOne);
            await this.boardsService.CreateNewBoardAsync(boardNameTwo);
            await this.boardsService.CreateNewBoardAsync(boardNameThree);

            // The boards will be ordered alphabetically
            List<BoardViewModel> boardsResult = await boardsService.GetAllBoardsAsync();

            int firstBoardIndex = 0;
            int secondBoardIndex = 1;
            int thirdBoardIndex = 2;

            Assert.That(boardsResult.Count == expectedBoardCount && boardsResult[firstBoardIndex].Name == boardNameOne &&
                        boardsResult[secondBoardIndex].Name == boardNameTwo && boardsResult[thirdBoardIndex].Name == boardNameThree,
                        $"The count of all boards in db be {expectedBoardCount} and the names of the boards must be identical.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_GetAllBoardsNamesAsync_Must_Return_True()
        {
            // The boards will be ordered alphabetically therefore the names must be in the same order.
            string boardNameOne = "In Progress";
            string boardNameTwo = "Open";
            string boardNameThree = "TODO";
            int expectedBoardCount = 3;

            await this.boardsService.CreateNewBoardAsync(boardNameOne);
            await this.boardsService.CreateNewBoardAsync(boardNameTwo);
            await this.boardsService.CreateNewBoardAsync(boardNameThree);

            // The boards will be ordered alphabetically
            List<string> boardsNamesAsync = await this.boardsService.GetAllBoardsNamesAsync();

            int firstBoardIndex = 0;
            int secondBoardIndex = 1;
            int thirdBoardIndex = 2;

            Assert.That(boardsNamesAsync.Count == expectedBoardCount && boardsNamesAsync[firstBoardIndex] == boardNameOne &&
                        boardsNamesAsync[secondBoardIndex] == boardNameTwo && boardsNamesAsync[thirdBoardIndex] == boardNameThree,
                        $"The count of all boards in db must be {expectedBoardCount} and the names of the boards must be identical.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_GetBoardsWithThierTasksAsync_Must_Return_True()
        {
            int expectedBoardsCount = 2;
            int expectedTasksCount = 1;
            int firstBoardIndex = 0;
            int secondBoardIndex = 1;
            int taskIndex = 0;
            string taskOneTitle = "Hygiene";
            string taskOneDescription = "To clean the house on Sunday.";
            string taskTwoTitle = "Free time";
            string taskTwoDescription = "To Jump with parachute";
            string boardOnename = "Future";
            string boardTwoname = "TODO";

            User userKichka = await HelperMethods.CreateUserWithNameKichka(dbContext);
            await this.boardsService.CreateNewBoardAsync(boardOnename);
            await this.boardsService.CreateNewBoardAsync(boardTwoname);
            await this.dbContext.SaveChangesAsync();

            Board? boardOne = await this.dbContext.Boards.FirstOrDefaultAsync(b => b.Name == boardOnename);
            Board? boardTwo = await this.dbContext.Boards.FirstOrDefaultAsync(b => b.Name == boardTwoname);

            Task taskOne = HelperMethods.CreateTask(boardOne, taskOneTitle, userKichka, taskOneDescription);
            Task taskTwo = HelperMethods.CreateTask(boardTwo, taskTwoTitle, userKichka, taskTwoDescription);

            await this.dbContext.Tasks.AddAsync(taskOne);
            await this.dbContext.Tasks.AddAsync(taskTwo);
            await this.dbContext.SaveChangesAsync();

            List<BoardWithTasksViewModel> boardsResult = await this.boardsService.GetBoardsWithThierTasksAsync();

            Assert.That(boardsResult.Count == expectedBoardsCount && boardsResult[firstBoardIndex].BoardName == boardOnename && boardsResult[secondBoardIndex].BoardName == boardTwoname &&
                        boardsResult[firstBoardIndex].Tasks.Count == expectedTasksCount && boardsResult[secondBoardIndex].Tasks.Count == expectedTasksCount &&
                         boardsResult[firstBoardIndex].Tasks[taskIndex].Id == taskOne.Id && boardsResult[secondBoardIndex].Tasks[taskIndex].Id == taskTwo.Id,
                        $"The count of the boards must be {expectedBoardsCount}. The count of their tasks must be {expectedTasksCount}. The names of the boards and their tasks titles must be identical.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_GetUsersBoardsAsync_Must_Return_True()
        {
            int expectedBoardsCount = 2;
            int expectedTasksCountForBoardOne = 1;
            int expectedTasksCountForBoardTwo = 0;
            int firstBoardIndex = 0;
            int secondBoardIndex = 1;
            string taskOneTitle = "Hygiene";
            string taskOneDescription = "To clean the house on Sunday.";
            string taskTwoTitle = "Free time";
            string taskTwoDescription = "To Jump with parachute";
            string boardOneName = "Future";
            string boardTwoName = "TODO";

            User userKichka = await HelperMethods.CreateUserWithNameKichka(this.dbContext);
            User userDimitrichko = await HelperMethods.CreateUserWithNameDimitrichko(this.dbContext);

            await this.boardsService.CreateNewBoardAsync(boardOneName);
            await this.boardsService.CreateNewBoardAsync(boardTwoName);
            await this.dbContext.SaveChangesAsync();

            Board? boardOne = await this.dbContext.Boards.FirstOrDefaultAsync(b => b.Name == boardOneName);
            Board? boardTwo = await this.dbContext.Boards.FirstOrDefaultAsync(b => b.Name == boardTwoName);

            Task taskOne = HelperMethods.CreateTask(boardOne, taskOneTitle, userKichka, taskOneDescription);
            Task taskTwo = HelperMethods.CreateTask(boardTwo, taskTwoTitle, userDimitrichko, taskTwoDescription);

            await this.dbContext.Tasks.AddAsync(taskOne);
            await this.dbContext.Tasks.AddAsync(taskTwo);
            await this.dbContext.SaveChangesAsync();

            List<BoardWithTasksCount> kichkaBoards = await this.boardsService.GetUserBoardsWithTasksCountAsync(userKichka.UserName);

            Assert.That(kichkaBoards.Count == expectedBoardsCount && kichkaBoards[firstBoardIndex].BoardName == boardOneName &&
                        kichkaBoards[firstBoardIndex].TasksCount == expectedTasksCountForBoardOne && kichkaBoards[secondBoardIndex].TasksCount == expectedTasksCountForBoardTwo,
                        $"The count of the boards must be {expectedBoardsCount}. The count of their tasks must be {expectedTasksCountForBoardOne}. The names of the boards and their tasks titles must be identical.");
        }
    }
}