namespace TaskBoardApp.Models
{
    public class UserTasksViewModel
    {
        public List<TaskViewModel> UsersTasks { get; set; } = new List<TaskViewModel>();

        public List<BoardTasksViewModel> UsersBoards { get; set; } = new List<BoardTasksViewModel>();
    }
}
