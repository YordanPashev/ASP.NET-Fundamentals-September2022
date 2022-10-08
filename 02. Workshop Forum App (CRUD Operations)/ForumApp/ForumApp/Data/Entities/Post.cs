namespace ForumApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Common;

    public class Post
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(GlobalConstants.PostTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.PostContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
