namespace TaskBoardApp.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Common;
    using Models;
    using Services.Contracts;

    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksService tasksService;
        private readonly IBoardsService boardsService;


        public TasksController(ITasksService _tasksService, IBoardsService boardsService)
        {
            this.tasksService = _tasksService;
            this.boardsService = boardsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string userMessage)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            UserTasksViewModel model = new UserTasksViewModel()
            {
                UsersTasks = await this.tasksService.GetUsersTasksAsync(User?.Identity?.Name),
                UsersBoards = await this.boardsService.GetUsersBoardsAsync(User?.Identity?.Name),
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string userMessage)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            CreateTaskViewModel model = new CreateTaskViewModel()
            {
                ExistingBoards = await this.boardsService.GetAllBoardsAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            if (!this.ModelState.IsValid || model == null || this.User?.Identity?.Name == null)
            {
                return this.RedirectToAction("Create", "Tasks", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            if (!await IsBoardValid(model.BoardId))
            {
                return this.RedirectToAction("Create", "Tasks", new { userMessage = GlobalConstants.InvalidBoardMessage });
            }

            model.OwnerUsername = this.User.Identity.Name;
            await this.tasksService.CreateNewTaskAsync(model);

            return this.RedirectToAction("Index", "Tasks", new { userMessage = GlobalConstants.NewTaskAddedMessage});
        }

        private async Task<bool> IsBoardValid(Guid boardId)
            => await this.tasksService.IsBoardExistsAsync(boardId);
    }
}
