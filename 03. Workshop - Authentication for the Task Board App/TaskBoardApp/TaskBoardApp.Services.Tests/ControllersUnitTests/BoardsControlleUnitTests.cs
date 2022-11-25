namespace TaskBoardAppTests.ControllersUnitTests
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    using TaskBoardApp.Common;
    using TaskBoardApp.Controllers;
    using TaskBoardApp.Models;
    using TaskBoardApp.Services.Contracts;

    [TestFixture]
    public class BoardsControlleUnitTests
    {        
        private const string UserMessage = "User message text.";

        private Mock<IBoardsService> boardsService;
        private BoardsController boardsController;
        private ViewResult? viewResult;
        private RedirectToActionResult? RedirectToActionResult;

        string? viewBagUserMessage;
        string? userMessage;


        [SetUp]
        public void SetUp()
        {
            this.boardsService = new Mock<IBoardsService>();
            this.boardsController = new BoardsController(boardsService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task Test_Index_Must_Return_ViewResult_With_Empty_ViewBag()
        {
            this.viewBagUserMessage = string.Empty;

            var result = await this.boardsController.Index();

            if (result.GetType() == typeof(ViewResult))
            {
                this.viewResult = result as ViewResult;
                viewBagUserMessage = this.viewResult?.ViewData["UserMessage"] == null ? string.Empty : this.viewResult?.ViewData?["UserMessage"]?.ToString();
            }

            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult), "The type of the result must be 'ViewResult'");
            Assert.IsEmpty(this.viewBagUserMessage, "The ViewBag.UserMessage must be empty.");
        }

        [Test]
        public async System.Threading.Tasks.Task Test_Index_Must_Return_ViewResult_With_ViewBag_UserMessage()
        {
            this.viewBagUserMessage = string.Empty;

            var result = await this.boardsController.Index(UserMessage);

            if (result.GetType() == typeof(ViewResult))
            {
                this.viewResult = result as ViewResult;
                this.viewBagUserMessage = this.viewResult?.ViewData["UserMessage"] == null ? string.Empty : this.viewResult?.ViewData?["UserMessage"]?.ToString();
            }

            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult), "Type of result must be 'ViewResult'.");
            Assert.That(this.viewBagUserMessage == UserMessage, $"ViewBag.UserMessage must be identical to '{UserMessage}'");
        }

        [Test]
        public void Test_CreateGet_Must_Return_ViewResult_With_Empty_ViewBag()
        {
            this.viewBagUserMessage = string.Empty;

            var result = this.boardsController.Create();

            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult), "The type of the result must be 'ViewResult'");
            Assert.IsEmpty(this.viewBagUserMessage, "The ViewBag.UserMessage must be empty.");
        }

        [Test]
        public void Test_CreateGet_Must_Return_ViewResult_With_ViewBag_UserMessage()
        {
            this.viewBagUserMessage = string.Empty;

            var result = this.boardsController.Create(UserMessage);

            if (result.GetType() == typeof(ViewResult))
            {
                this.viewResult = result as ViewResult;
                this.viewBagUserMessage = this.viewResult?.ViewData["UserMessage"] == null ? string.Empty : this.viewResult?.ViewData?["UserMessage"]?.ToString();
            }

            Assert.IsNotNull(result, "The type of the result must be 'ViewResult'");
            Assert.That(result.GetType() == typeof(ViewResult), "Type of result must be 'ViewResult'.");
            Assert.That(this.viewBagUserMessage == UserMessage, $"ViewBag.UserMessage must be identical to '{UserMessage}'");
        }

        [TestCase("Create board name with to long name")]
        [TestCase("ss")]
        [TestCase(null)]
        [TestCase("")]

        public async System.Threading.Tasks.Task Test_CreatePost_Must_Return_ViewResult_With_ViewBag_UserMessage_InvalidData(string boardName)
        {
            CreateBoardViewModel model = new CreateBoardViewModel()
            {
                Name = boardName,
            };

            boardsController.ModelState.AddModelError("Name", "The must be at least 3 chars at max 20.");
            var result = await this.boardsController.Create(model);

            if (result.GetType() == typeof(RedirectToActionResult))
            {
                this.RedirectToActionResult = result as RedirectToActionResult;
                this.userMessage = ((object[])this.RedirectToActionResult.RouteValues.Values)[0].ToString();
            }

            Assert.IsNotNull(result, "The type of the result must be 'RedirectToActionResult'");
            Assert.That(result.GetType() == typeof(RedirectToActionResult), "The type of the result must be 'ViewResult'");
            Assert.That(this.userMessage == GlobalConstants.InvalidDataMessage, $"ViewBag.UserMessage must be identical to '{GlobalConstants.InvalidDataMessage}'");
        }

        [TestCase("TODO")]
        [TestCase("Fut")]
        [TestCase("In Progress")]
        [TestCase("This will contains 29 chars.")]
        public async System.Threading.Tasks.Task Test_CreatePost_Must_Return_ViewResult_With_Empty_ViewBag(string boardName)
        {
            CreateBoardViewModel model = new CreateBoardViewModel()
            {
                Name = boardName,
            };

            var result = await this.boardsController.Create(model);

            if (result.GetType() == typeof(RedirectToActionResult))
            {
                this.RedirectToActionResult = result as RedirectToActionResult;
                this.userMessage = ((object[])this.RedirectToActionResult.RouteValues.Values)[0].ToString();
            }

            Assert.IsNotNull(result, "The type of the result must be 'RedirectToActionResult'");
            Assert.That(result.GetType() == typeof(RedirectToActionResult), "The type of the result must be 'ViewResult'");
            Assert.That(this.userMessage == GlobalConstants.NewBoardAddedMessage, $"ViewBag.UserMessage must be identical to '{GlobalConstants.NewBoardAddedMessage}'");

        }
    }
}
