namespace TaskBoardApp.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Services.Contracts;

    public class HomeController : Controller
    {

        private readonly IBoardsService boardsService;
        private readonly ILogger<HomeController> logger;
        private readonly ITasksService tasksService;

        public HomeController(ILogger<HomeController> _logger, IBoardsService _boardsService, ITasksService _tasksService)
        {
            this.logger = _logger;
            this.boardsService = _boardsService;
            this.tasksService = _tasksService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                AllBoardTasksWithUserTasksCountViewModel model = new AllBoardTasksWithUserTasksCountViewModel()
                {
                    BoardTasks = await this.boardsService.GetBoardsWithThierTasksAsync(),
                    UsersTasksCount = await this.tasksService.GetUsersTasksCountAsync(User?.Identity?.Name),
                    AllTasksCount = await this.tasksService.GetAllTasksCount(),
                };

                return this.View(model);
            }

            return this.View(new AllBoardTasksWithUserTasksCountViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}