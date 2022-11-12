namespace TaskBoardApp.Models
{
    public class AllBoardTasksWithUserTasksCountViewModel
    {
        public List<BoardTasksViewModel> BoardTasks { get; set; } = new List<BoardTasksViewModel>();

        public int UsersTasksCount { get; set; }

        public int AllTasksCount { get; set;}
    }
}
