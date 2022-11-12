namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface IBoardsService
    {
        Task CreateNewBoardAsync(string boardName);

        Task<List<BoardViewModel>> GetAllBoardsAsync();

        Task<List<string>> GetAllBoardsNamesAsync();

        Task<List<BoardTasksViewModel>> GetBoardsWithThierTasksAsync();

        Task<List<BoardTasksViewModel>> GetUsersBoardsAsync(string? userName);
    }
}
