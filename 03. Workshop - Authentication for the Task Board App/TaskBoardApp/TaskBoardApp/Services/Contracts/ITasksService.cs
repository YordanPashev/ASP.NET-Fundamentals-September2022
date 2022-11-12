namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName);

        Task<bool> IsBoardExistsAsync(Guid boardId);

        Task CreateNewTaskAsync(CreateTaskViewModel model);
    }
}
