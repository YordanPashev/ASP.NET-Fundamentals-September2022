namespace TaskBoardApp.Models
{

    using Models.Boards;

    public class UserTasksAndBoardsViewModel
    {
        public List<TaskViewModel> UsersTasks { get; set; } = new List<TaskViewModel>();

        public List<BoardWithTasksCount> UsersBoards { get; set; } = new List<BoardWithTasksCount>();
    }
}