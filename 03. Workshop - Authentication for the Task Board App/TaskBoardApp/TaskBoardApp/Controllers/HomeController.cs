namespace TaskBoardApp.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Data.Entities;
    using Models;
    using Services.Contracts;

    public class HomeController : Controller
    {

        private readonly IBoardsService boardsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBoardsService _boardsService)
        {
            this._logger = logger;
            this.boardsService = _boardsService;
        }

        public async Task<IActionResult> Index()
        {
            List<BoardTasksViewModel> model = await this.boardsService.GetBoardTasksAsync();
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}