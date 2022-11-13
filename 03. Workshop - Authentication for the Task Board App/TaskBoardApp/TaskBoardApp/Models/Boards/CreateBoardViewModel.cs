namespace TaskBoardApp.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class CreateBoardViewModel
    {
        [Required]
        [MinLength(GlobalConstants.BoardNameMinLength)]
        [MaxLength(GlobalConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
