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

            UserTasksAndBoardsViewModel model = await this.tasksService.GetUsersTasksAndBordsAsync(User?.Identity?.Name);

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
            if (!this.ModelState.IsValid || !await IsBoardValidAsync(model.BoardId))
            {
                return this.RedirectToAction("Create", "Tasks", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            model.OwnerUsername = this.User?.Identity?.Name;
            await this.tasksService.CreateNewTaskAsync(model);

            return this.RedirectToAction("Index", "Tasks", new { userMessage = GlobalConstants.NewTaskAddedMessage });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string taskId = id.ToString();
            TaskViewModel? model = await this.tasksService.GetTaskByAdAsync(taskId.ToString());

            if (taskId.ToString() == null || model == null)
            {
                return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(!await this.tasksService.TryDeleteTaskById(id.ToString()))
            {
                return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.InvalidDataMessage });
            }
    
            return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.SuccessfullyDeletedTaskMessage });
        }
    

        private async Task<bool> IsBoardValidAsync(Guid boardId)
            => await this.tasksService.IsBoardExistsAsync(boardId);
    }
}
