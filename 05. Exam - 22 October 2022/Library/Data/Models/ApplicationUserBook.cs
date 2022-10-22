namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUserBook
    {
        [Required]
        [ForeignKey(nameof(ApplicationUserId))]

        public string ApplicationUserId { get; set; } = null!;

        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        [ForeignKey(nameof(BookId))]

        public int BookId { get; set; }

        public Book? Book { get; set; } 
    }
}
