namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface IBoardsService
    {
        Task CreateNewBoardAsync(string boardName);

        Task<List<BoardViewModel>> GetAllBoardsAsync();

        Task<List<string>> GetAllBoardsNamesAsync();

        Task<List<BoardWithTasksViewModel>> GetBoardsWithThierTasksAsync();

        Task<List<BoardWithTasksViewModel>> GetUsersBoardsAsync(string? userName);
    }
}
