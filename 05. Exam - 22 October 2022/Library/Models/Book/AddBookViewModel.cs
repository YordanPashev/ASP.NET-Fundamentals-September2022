namespace Library.Models.Book
{
    using System.ComponentModel.DataAnnotations;

    using Library.Common;
    using Library.Data.Models;

    public class AddBookViewModel
    {
        [Required]
        [MinLength(GlobalConstants.BookTitleMinLength)]
        [MaxLength(GlobalConstants.BookTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.BookAuthorMinLength)]
        [MaxLength(GlobalConstants.BookAuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.BookDescriptionMinLength)]
        [MaxLength(GlobalConstants.BookDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), GlobalConstants.BookRaitingMinValue, GlobalConstants.BookRaitingMaxValue)]
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

