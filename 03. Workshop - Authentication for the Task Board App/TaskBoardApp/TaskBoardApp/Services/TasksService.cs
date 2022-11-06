namespace TaskBoardApp.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services.Contracts;

    public class TasksService : ITasksService
    {
        private readonly TaskBoardDbContext context;

        public TasksService(TaskBoardDbContext dbcontext)
            => this.context = dbcontext;

        public Task CreateNewTask(string boardName)
        {
            throw new NotImplementedException();
        }

        public async Task<Data.Entities.Task[]> GetAllTasks()
            => await this.context.Tasks
                        .Include(t => t.Owner)
                        .Include(t => t.Board)
                        .OrderBy(t=> t.Title)
                        .ToArrayAsync();
    }
}
