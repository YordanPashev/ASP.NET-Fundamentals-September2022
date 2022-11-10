namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task<List<TaskViewModel>> GetUsersTasks(string? userName);

        Task<bool> IsBoardExists(Guid boardId);

        Task CreateNewTask(CreateTaskViewModel model);
    }
}
