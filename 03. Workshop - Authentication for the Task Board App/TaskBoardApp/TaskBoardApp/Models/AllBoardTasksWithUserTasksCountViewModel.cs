namespace TaskBoardApp.Models
{
    public class AllBoardTasksWithUserTasksCountViewModel
    {
        public List<BoardWithTasksViewModel> BoardTasks { get; set; } = new List<BoardWithTasksViewModel>();

        public int UsersTasksCount { get; set; }

        public int AllTasksCount { get; set;}
    }
}
