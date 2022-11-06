namespace TaskBoardApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

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

        [Required]
        [ForeignKey(nameof(Board))]
        public Guid BoardId { get; set; }
        public Board Board { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { set; get; } = null!;
        public User Owner { get; set; } = null!;
    }
}
