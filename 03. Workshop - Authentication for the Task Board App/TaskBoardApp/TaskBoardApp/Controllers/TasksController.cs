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
        public async Task<IActionResult> Details(Guid? id)
        {
            TaskViewModel? model = await this.tasksService.CreateTaskViewModelByIdAsync(id.ToString());

            if (model == null)
            {
                return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(!await this.tasksService.TryToDeleteTaskById(id.ToString()))
            {
                return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.InvalidDataMessage });
            }
    
            return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.SuccessfullyDeletedTaskMessage });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id, string userMessage)
        {
            EditTaskViewModel model = await this.tasksService.CreateNewEditTaskViewModelByIdAsync(Id);

            if (model == null || model.OwnerUsername != this.User?.Identity?.Name)
            {
                return this.RedirectToAction("Index", "Home", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTaskViewModel model)
        {
            Data.Entities.Task? task = await this.tasksService.GetTaskByIdAsync(model.Id.ToString());

            if (!this.ModelState.IsValid || !await IsBoardValidAsync(model.BoardId) || task == null)
            {
                return this.RedirectToAction("Edit", "Tasks", new { userMessage = GlobalConstants.InvalidDataMessage });
            }

            if (task.Title == model.Title && task.Description == model.Description && task.BoardId == model.BoardId)
            {
                return this.RedirectToAction("Edit", "Tasks", new { userMessage = GlobalConstants.PleaseEditSelectedTaskUserMessage, Id = model.Id});
            }
            await this.tasksService.EditTaskAsync(model);

            return this.RedirectToAction("Index", "Tasks", new { userMessage = GlobalConstants.SuccessfullyEditedTaskMessage });
        }

        private async Task<bool> IsBoardValidAsync(Guid boardId)
            => await this.tasksService.IsBoardExistsAsync(boardId);
    }
}
