namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task<List<TaskViewModel>> GetAllTasks();

        Task<bool> IsBoardExists(Guid boardId);

        Task CreateNewTask(CreateTaskViewModel model);
    }
}
