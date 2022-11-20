namespace TaskBoardApp.Services.Contracts
{
    using Models;
    using Models.Boards;

    public interface IBoardsService
    {
        Task CreateNewBoardAsync(string boardName);

        Task<List<BoardViewModel>> GetAllBoardsAsync();

        Task<List<string>> GetAllBoardsNamesAsync();

        Task<List<BoardWithTasksViewModel>> GetBoardsWithThierTasksAsync();

        Task<List<BoardWithTasksCount>> GetUserBoardsWithTasksCountAsync(string? userName);
    }
}
