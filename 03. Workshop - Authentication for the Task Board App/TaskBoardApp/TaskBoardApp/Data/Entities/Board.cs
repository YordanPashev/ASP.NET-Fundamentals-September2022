namespace TaskBoardApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Board
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual List<Task> Tasks { get; set; } = new List<Task>();
    }
}
