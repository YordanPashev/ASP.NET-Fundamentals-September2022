namespace ForumApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Common;

    public class Post
    {
        public Post()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(GlobalConstants.PostTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.PostContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? EditedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public string Author { get; set; } = null!;
    }
}
