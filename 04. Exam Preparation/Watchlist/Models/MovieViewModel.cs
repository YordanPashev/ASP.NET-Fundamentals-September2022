namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MovieViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [Required]
        public string Genre { get; set; } = null!;
    }
}
