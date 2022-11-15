namespace TaskBoardApp.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Entities;
    using Models;
    using Services.Contracts;
    using TaskBoardApp.Common;

    public class BoardsService : IBoardsService
    {
        private readonly TaskBoardDbContext context;

        public BoardsService(TaskBoardDbContext dbcontext)
            => this.context = dbcontext;

        public async System.Threading.Tasks.Task CreateNewBoardAsync(string boardName)
        {
            Board board = new Board()
            {
                Id = Guid.NewGuid(),
                Name = boardName,
            };

            this.context.Boards.Add(board);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<BoardViewModel>> GetAllBoardsAsync()
           => await this.context.Boards
                            .Select(b => new BoardViewModel() { Id = b.Id, Name = b.Name, })
                            .OrderBy(b => b.Name)
                            .ToListAsync();

        public async Task<List<string>> GetAllBoardsNamesAsync()
            => await this.context.Boards
                            .Select(b => b.Name)
                            .OrderBy(b => b)
                            .ToListAsync();

        public async Task<List<BoardWithTasksViewModel>> GetBoardsWithThierTasksAsync()
        {
            List<string> boardsNames = new List<string>();

            return await this.context.Boards
                            .Include(b => b.Tasks)
                            .Select(b => new BoardWithTasksViewModel()
                            {
                                BoardName = b.Name,
                                TasksCount = b.Tasks.Count(),
                                Tasks = b.Tasks.Select(t => new TaskViewModel()
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Description = t.Description,
                                    CreatedOn = t.CreatedOn.ToString(GlobalConstants.TaskDateTimeFormat),
                                    BoardName = b.Name,
                                    OwnerUsername = t.Owner.UserName,
                                })
                                .ToList()
                            })
                            .OrderBy(b => b.BoardName)
                            .ToListAsync();
        }
    }
}
