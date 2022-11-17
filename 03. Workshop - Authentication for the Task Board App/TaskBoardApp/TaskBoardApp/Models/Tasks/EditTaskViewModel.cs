namespace TaskBoardApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditTaskViewModel : CreateTaskViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
