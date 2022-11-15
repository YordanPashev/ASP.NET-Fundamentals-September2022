namespace TaskBoardApp.Models
{
    public class UserTasksAndBoardsViewModel
    {
        public List<TaskViewModel> UsersTasks { get; set; } = new List<TaskViewModel>();

        public List<BoardWithTasksViewModel> UsersBoards { get; set; } = new List<BoardWithTasksViewModel>();
    }
}