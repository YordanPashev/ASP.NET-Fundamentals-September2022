namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Library.Common;

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.BookTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.BookAuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.BookDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public List<ApplicationUserBook> ApplicationUsersBooks { get; set; } = new List<ApplicationUserBook>();
    }
}
