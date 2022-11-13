namespace TaskBoardApp.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        
        public string? CreatedOn { get; set; }
      
        public string BoardName { get; set; } = null!;

        public string OwnerUsername { set; get; } = null!;
    }
}
