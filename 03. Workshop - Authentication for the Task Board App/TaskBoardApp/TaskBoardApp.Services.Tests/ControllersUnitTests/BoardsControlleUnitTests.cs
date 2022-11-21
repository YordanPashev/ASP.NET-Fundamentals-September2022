namespace TaskBoardAppTests.ControllersUnitTests
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    using TaskBoardApp.Controllers;
    using TaskBoardApp.Services.Contracts;

    [TestFixture]
    public class BoardsControlleUnitTests
    {
        private Mock<IBoardsService> boardsService;
        private BoardsController boardsController;

        [SetUp]
        public void SetUp()
        {
            this.boardsService = new Mock<IBoardsService>();
            this.boardsController = new BoardsController(boardsService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task Test_Index_Must_Return_ViewResult_With_Empty_ViewBag()
        {
            IActionResult result = await this.boardsController.Index();

            Assert.That(result.GetType() == typeof(ViewResult));           
            Assert.IsNull(this.boardsController.ViewBag.UserMessage, "The ViewBag.UserMessage must be empty.");
        }

        [Test]
        public async System.Threading.Tasks.Task TTest_Index_Must_Return_ViewResult_With_ViewBag_UserMessage()
        {
            string userMessage = "User message text.";
            IActionResult result = await this.boardsController.Index(userMessage);

            Assert.That(result.GetType() == typeof(ViewResult), "Type of result must be 'ViewResult'.");
            Assert.That(this.boardsController.ViewBag.UserMessage == userMessage, $"ViewBag.UserMessage must be identical to '{userMessage}'");
        }
    }
}
