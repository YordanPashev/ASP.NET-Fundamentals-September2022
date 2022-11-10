namespace TaskBoardApp.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services.Contracts;
    using Models;
    using Data.Entities;
    using Common;

    public class TasksService : ITasksService
    {
        private readonly TaskBoardDbContext context;

        public TasksService(TaskBoardDbContext dbcontext)
            => this.context = dbcontext;

        public async System.Threading.Tasks.Task CreateNewTask(CreateTaskViewModel model)
        {
            User? owner = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == model.OwnerUsername);

            Data.Entities.Task task = new Data.Entities.Task()
            {
                Id = new Guid(),
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = owner.Id,
            };

            await this.context.Tasks.AddAsync(task);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<TaskViewModel>> GetUsersTasks(string? userName)
        {
            User? user = this.context.Users.FirstOrDefault(u => u.UserName == userName);
                
            return await this.context.Tasks
                                .Include(t => t.Board)
                                .Where(t => t.OwnerId == user.Id)
                                .Select(t =>
                                    new TaskViewModel()
                                    {
                                        Id = t.Id,
                                        Title = t.Title,
                                        Description = t.Description,
                                        CreatedOn = t.CreatedOn.ToString(GlobalConstants.TaskDateTimeFormat),
                                        BoardName = t.Board.Name,
                                        OwnerUsername = t.Owner.UserName
                                    })
                                .OrderBy(t => t.Title)
                                .ToListAsync();
        }
           

        public async Task<bool> IsBoardExists(Guid boardId)
            => await this.context.Boards.AnyAsync(b => b.Id == boardId);
    }
}
