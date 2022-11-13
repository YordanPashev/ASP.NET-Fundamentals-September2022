namespace TaskBoardApp.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Entities;
    using Models;
    using Services.Contracts;

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


        public async Task<List<BoardTasksViewModel>> GetBoardsWithThierTasksAsync()
        {
            List<string> boardsNames = new List<string>();

            return await this.context.Boards
                            .Include(b => b.Tasks)
                            .Select(b => new BoardTasksViewModel()
                            {
                                BoardName = b.Name,
                                TasksCount = b.Tasks.Count()
                            })
                            .OrderBy(b => b.BoardName)
                            .ToListAsync();
        }

        public async Task<List<BoardTasksViewModel>> GetUsersBoardsAsync(string? userName)
        {
            List<string> boardsNames = new List<string>();
            User? user = await this.context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            return await this.context.Boards
                            .Include(b => b.Tasks)
                            .Select(b => new BoardTasksViewModel()
                            {
                                BoardName = b.Name,
                                TasksCount = b.Tasks.Where(t => t.OwnerId == user.Id).Count()      
                            })
                            .OrderBy(b => b.BoardName)
                            .ToListAsync();
        }
    }
}
