namespace TaskBoardApp.Models
{
    public class UserTasksViewModel
    {
        public List<TaskViewModel> UsersTasks { get; set; } = new List<TaskViewModel>();

        public List<BoardWithTasksViewModel> UsersBoards { get; set; } = new List<BoardWithTasksViewModel>();
    }
}
