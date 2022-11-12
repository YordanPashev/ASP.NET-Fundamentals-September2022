namespace TaskBoardApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Common;
    using Models;
    using Services.Contracts;

    [Authorize]
    public class BoardsController : Controller
    {
        private readonly IBoardsService boardsService;

        public BoardsController(IBoardsService _boardsService)
            => this.boardsService = _boardsService;

        [HttpGet]
        public async Task<IActionResult> Index(string? userMessage = null)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            this.ViewBag.UserMessage = userMessage;
            List<string> model = await this.boardsService.GetAllBoardsNamesAsync();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create(string? userMessage = null)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            return this.View(new CreateBoardViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBoardViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Boards", new { userMessage = GlobalConstants.NewBoardAddedMessage });
            }

            await this.boardsService.CreateNewBoardAsync(model.Name);

            return this.RedirectToAction("Index", "Boards", new { userMessage = GlobalConstants.NewBoardAddedMessage });
        }
    }
}
