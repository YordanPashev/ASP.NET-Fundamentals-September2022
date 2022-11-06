namespace TaskBoardApp.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Entities;

    public class TaskViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.TaskTitleMinLength)]
        [MaxLength(GlobalConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.TaskDescriptionMinLength)]
        [MaxLength(GlobalConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string BoardName { get; set; } = null!;

        [Required]
        public string OwnerUsername { set; get; } = null!;
    }
}
