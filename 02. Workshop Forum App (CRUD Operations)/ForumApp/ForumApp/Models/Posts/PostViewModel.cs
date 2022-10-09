namespace ForumApp.Models.Posts
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Common;

    public class PostViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.PostTitleMinLength)]
        [MaxLength(GlobalConstants.PostTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.PostContentMinLength)]
        [MaxLength(GlobalConstants.PostContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? EditedOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
