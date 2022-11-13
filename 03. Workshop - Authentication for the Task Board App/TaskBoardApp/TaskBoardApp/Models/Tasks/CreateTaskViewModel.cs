namespace TaskBoardApp.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class CreateTaskViewModel
    {
        [Required]
        [MinLength(GlobalConstants.TaskTitleMinLength)]
        [MaxLength(GlobalConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.TaskDescriptionMinLength)]
        [MaxLength(GlobalConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        [Required]
        public Guid BoardId { get; set; }

        public string? OwnerUsername { set; get; }

        public virtual List<BoardViewModel> ExistingBoards { get; set; } = new List<BoardViewModel>();
    }
}
