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
        private readonly IBoardsService boardsService;


        public TasksService(TaskBoardDbContext dbcontext, IBoardsService boardsService)
        {
            this.context = dbcontext;
            this.boardsService = boardsService;
        }

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
                OwnerId =owner.Id
            };

            await this.context.Tasks.AddAsync(task);
            await this.context.SaveChangesAsync();
        }

        public async Task<TaskViewModel?> CreateTaskViewModelByIdAsync(string? taskId)
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

        public async Task<bool> TryToDeleteTaskById(string taskId)
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

        public async Task<int> GetUsersTasksCountAsync(string? userName)
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

        public async Task<Data.Entities.Task?> GetTaskByIdAsync(string? taskId)
            => await this.context.Tasks
                           .Include(t => t.Owner)
                           .Include(t => t.Board)
                           .FirstOrDefaultAsync(t => t.Id.ToString() == taskId);

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

        public async System.Threading.Tasks.Task EditTaskAsync(EditTaskViewModel model)
        {
            Data.Entities.Task? task = await this.context.Tasks.FirstOrDefaultAsync(t => t.Id == model.Id);

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await this.context.SaveChangesAsync();
        }

        public async Task<EditTaskViewModel?> CreateNewEditTaskViewModelByIdAsync(Guid modelId)
        {
            Data.Entities.Task? task = await this.context.Tasks.Include(t => t.Owner).FirstOrDefaultAsync(t => t.Id == modelId);

            if (task == null)
            {
                return null;
            }

            return new EditTaskViewModel()
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            BoardId = task.BoardId,
                            OwnerUsername = task.Owner.UserName,
                            ExistingBoards = await this.boardsService.GetAllBoardsAsync()
                        };
        }

    }
}
