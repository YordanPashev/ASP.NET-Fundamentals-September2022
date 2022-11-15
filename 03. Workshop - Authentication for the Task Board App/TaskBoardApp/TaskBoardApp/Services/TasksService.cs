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

        public async System.Threading.Tasks.Task CreateNewTaskAsync(CreateTaskViewModel model)
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

        public async Task<bool> TryDeleteTaskById(string taskId)
        {
            Data.Entities.Task? task = await this.context.Tasks.FirstOrDefaultAsync(t => t.Id.ToString() == taskId);

            if (task == null)
            {
                return false;
            }

            this.context.Tasks.Remove(task);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsBoardExistsAsync(Guid boardId)
            => await this.context.Boards.AnyAsync(b => b.Id == boardId);


        public async Task<int> GetAllTasksCount()
            => await this.context.Tasks.CountAsync();

        public async Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName)
        {
            User? user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

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
                                        OwnerUsername = t.Owner.UserName,
                                    })
                                .OrderBy(t => t.Title)
                                .ToListAsync();
        }

        public async Task<int> GetUsersTasksCountAsync(string userName)
        {
            User? user = this.context.Users.FirstOrDefault(u => u.UserName == userName);

            return await this.context.Tasks.Where(t => t.OwnerId == user.Id).CountAsync();
        }

        public async Task<UserTasksAndBoardsViewModel> GetUsersTasksAndBordsAsync(string? userName)
            => new UserTasksAndBoardsViewModel()
            {
                UsersTasks = await GetUsersTasksAsync(userName),
                UsersBoards = await GetUsersBoardsAsync(userName),
            };

        public async Task<TaskViewModel?> GetTaskByAdAsync(string? taskId)
        {
            Data.Entities.Task? task = await this.context.Tasks
                                .Include(t => t.Owner)
                                .Include(t => t.Board)
                                .FirstOrDefaultAsync(t => t.Id.ToString() == taskId);

            if (task != null)
            {
                return new TaskViewModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    CreatedOn = task.CreatedOn.ToString(GlobalConstants.TaskDateTimeFormat),
                    BoardName = task.Board.Name,
                    OwnerUsername = task.Owner.UserName,
                };
            }

            return null;
        }

        private async Task<List<BoardWithTasksViewModel>> GetUsersBoardsAsync(string? userName)
        {
            List<string> boardsNames = new List<string>();
            User? user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            return await this.context.Boards
                            .Include(b => b.Tasks)
                            .Select(b => new BoardWithTasksViewModel()
                            {
                                BoardName = b.Name,
                                TasksCount = b.Tasks.Where(t => t.OwnerId == user.Id).Count()
                            })
                            .OrderBy(b => b.BoardName)
                            .ToListAsync();
        }
    }
}
