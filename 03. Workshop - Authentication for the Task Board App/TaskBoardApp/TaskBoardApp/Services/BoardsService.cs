namespace TaskBoardApp.Services
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using TaskBoardApp.Data;
    using TaskBoardApp.Data.Entities;
    using TaskBoardApp.Services.Contracts;

    public class BoardsService : IBoardsService
    {
        private readonly TaskBoardDbContext context;

        public BoardsService(TaskBoardDbContext dbcontext)
            => this.context = dbcontext;

        public async System.Threading.Tasks.Task CreateNewBoard(string boardName)
        {
            Board board = new Board()
            {
                Id = Guid.NewGuid(),
                Name = boardName,
            };

            this.context.Boards.Add(board);
            await this.context.SaveChangesAsync();
        }

        public async Task<string[]> GetAllBoards()
            => await this.context.Boards.Select(b => b.Name).OrderBy(b => b).ToArrayAsync();
    }
}
