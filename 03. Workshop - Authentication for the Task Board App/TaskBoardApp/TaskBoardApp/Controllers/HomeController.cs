namespace TaskBoardApp.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Services.Contracts;

    public class HomeController : Controller
    {

        private readonly IBoardsService boardsService;
        private readonly ITasksService tasksService;

        public HomeController(IBoardsService _boardsService, ITasksService _tasksService)
        {
            this.boardsService = _boardsService;
            this.tasksService = _tasksService;
        }

        public async Task<IActionResult> Index(string userMessage)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                BoardsWithUserTasksCountViewModel model = new BoardsWithUserTasksCountViewModel()
                {
                    Boards = await this.boardsService.GetBoardsWithThierTasksAsync(),
                    UsersTasksCount = await this.tasksService.GetUsersTasksCountAsync(User?.Identity?.Name),
                    AllTasksCount = await this.tasksService.GetAllTasksCount(),
                };

                return this.View(model);
            }

            return this.View(new BoardsWithUserTasksCountViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}