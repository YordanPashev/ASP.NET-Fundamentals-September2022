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
            var viewResult = result as ViewResult;

            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult));           
            Assert.IsNull(viewResult?.ViewData["UserMessage"], "The ViewBag.UserMessage must be empty.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_Index_Must_Return_ViewResult_With_ViewBag_UserMessage()
        {
            string userMessage = "User message text.";
            IActionResult result = await this.boardsController.Index(userMessage);
            var viewResult = result as ViewResult;
            string? viewBagResult = viewResult?.ViewData["UserMessage"] == null ? string.Empty : viewResult?.ViewData?["UserMessage"]?.ToString();
            
            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult), "Type of result must be 'ViewResult'.");
            Assert.That(viewBagResult == userMessage, $"ViewBag.UserMessage must be identical to '{userMessage}'");
        }
    }
}
