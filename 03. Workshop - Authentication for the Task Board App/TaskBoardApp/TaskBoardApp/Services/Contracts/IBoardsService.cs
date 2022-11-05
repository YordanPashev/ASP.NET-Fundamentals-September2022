namespace TaskBoardApp.Services.Contracts
{
    public interface IBoardsService
    {
        Task<string[]> GetAllBoards();

        Task CreateNewBoard(string boardName);
    }
}
