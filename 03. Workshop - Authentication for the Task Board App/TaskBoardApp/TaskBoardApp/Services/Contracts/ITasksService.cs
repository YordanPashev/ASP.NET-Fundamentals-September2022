﻿namespace TaskBoardApp.Services.Contracts
{
    using Models;

    public interface ITasksService
    {
        Task CreateNewTaskAsync(CreateTaskViewModel model);

        Task<bool> TryDeleteTaskById(string taskId);

        Task<bool> IsBoardExistsAsync(Guid boardId);

        Task<int> GetAllTasksCount();

        Task<List<TaskViewModel>> GetUsersTasksAsync(string? userName);

        Task<int> GetUsersTasksCountAsync(string userName);

        Task<TaskViewModel?> GetTaskByAdAsync(string? taskId);

        Task<UserTasksAndBoardsViewModel> GetUsersTasksAndBordsAsync(string? userName);
    }
}
