namespace TaskBoardApp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Services.Contracts;

    public class TasksController : Controller
    {
        private readonly ITasksService tasksService;

        public TasksController(ITasksService _tasksService)
            => this.tasksService = _tasksService;

        public async Task<IActionResult> Index()
        {
            TaskViewModel[] model = await GetAllExistingTasks();

            return View(model);
        }

        private async Task<TaskViewModel[]> GetAllExistingTasks()
        {
            Data.Entities.Task[] tasks = await this.tasksService.GetAllTasks();
            return tasks
                .Select(t => new TaskViewModel()
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        CreatedOn = t.CreatedOn,
                        BoardName = t.Board.Name,
                        OwnerUsername = t.Owner.UserName
                    })
                .ToArray();
        }
    }
}
