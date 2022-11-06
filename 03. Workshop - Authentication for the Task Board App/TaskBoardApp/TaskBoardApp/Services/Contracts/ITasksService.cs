namespace TaskBoardApp.Services.Contracts
{
    public interface ITasksService
    {
        Task<Data.Entities.Task[]> GetAllTasks();

        Task CreateNewTask(string boardName);
    }
}
