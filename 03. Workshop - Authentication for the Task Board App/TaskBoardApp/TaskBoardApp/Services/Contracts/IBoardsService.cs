namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface IBoardsService
    {
        Task<List<string>> GetAllBoardsNames();

        Task<List<BoardViewModel>> GetAllBoards();

        Task CreateNewBoard(string boardName);
    }
}
