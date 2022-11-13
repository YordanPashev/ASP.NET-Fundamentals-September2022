namespace TaskBoardApp.Models
{
    public class BoardWithTasksViewModel
    {
        public string BoardName { get; set; } = null!;

        public int TasksCount { get; set; }

        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
