namespace TaskBoardApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TaskBoardApp.Common;

    public class Task
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        public Guid BoardId { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { set; get; } = null!;

        [Required]
        public User Owner { get; set; } = null!;
    }
}
