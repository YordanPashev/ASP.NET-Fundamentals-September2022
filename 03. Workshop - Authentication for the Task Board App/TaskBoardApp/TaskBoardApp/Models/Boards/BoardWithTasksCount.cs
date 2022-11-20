namespace TaskBoardApp.Models.Boards
{
    public class BoardWithTasksCount
    {
        public string BoardName { get; set; } = null!;

        public int TasksCount { get; set; }
    }
}
