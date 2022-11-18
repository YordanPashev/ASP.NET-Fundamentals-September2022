namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task CreateNewTaskAsync(CreateTaskViewModel model);

        Task<TaskViewModel> CreateTaskViewModelByIdAsync(string? taskId);

        Task<EditTaskViewModel> CreateNewEditTaskViewModelByIdAsync(Guid modelId);

        Task EditTaskAsync(EditTaskViewModel model);

        Task<int> GetUsersTasksCountAsync(string? userName);

        Task<bool> TryToDeleteTaskById(string taskId);

        Task<bool> IsBoardExistsAsync(Guid boardId);

        Task<int> GetAllTasksCount();

        Task<Data.Entities.Task?> GetTaskByIdAsync(string? taskId);

        Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName);

        Task<UserTasksAndBoardsViewModel> GetUsersTasksAndBordsAsync(string? userName);

    }
}
