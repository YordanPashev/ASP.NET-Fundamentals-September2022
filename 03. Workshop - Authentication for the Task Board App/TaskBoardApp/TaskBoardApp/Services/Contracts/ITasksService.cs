namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task<EditTaskViewModel> CreateNewEditTaskViewModelByIdAsync(Guid? modelId);

        Task CreateNewTaskAsync(CreateTaskViewModel model);

        Task<TaskViewModel> CreateTaskViewModelByIdAsync(string? taskId);

        Task EditTaskAsync(EditTaskViewModel model);

        Task<int> GetUsersTasksCountAsync(string? userName);

        Task<int> GetAllTasksCount();

        Task<Data.Entities.Task?> GetTaskByIdAsync(string? taskId);

        Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName);

        Task<bool> IsBoardExistsAsync(Guid boardId);

        Task<bool> TryToDeleteTaskById(string taskId);


    }
}
