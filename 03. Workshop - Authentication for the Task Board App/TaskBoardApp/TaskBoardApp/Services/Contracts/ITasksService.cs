namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task CreateNewTaskAsync(CreateTaskViewModel model);

        Task<bool> IsBoardExistsAsync(Guid boardId);

        Task<int> GetAllTasksCount();

        Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName);

        Task<int> GetUsersTasksCountAsync(string userName);
    }
}
