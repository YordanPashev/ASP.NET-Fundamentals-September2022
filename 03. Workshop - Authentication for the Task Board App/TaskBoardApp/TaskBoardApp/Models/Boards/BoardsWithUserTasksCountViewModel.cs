namespace TaskBoardApp.Models
{
    public class BoardsWithUserTasksCountViewModel
    {
        public List<BoardWithTasksViewModel> Boards { get; set; } = new List<BoardWithTasksViewModel>();

        public int UsersTasksCount { get; set; }

        public int AllTasksCount { get; set;}
    }
}
