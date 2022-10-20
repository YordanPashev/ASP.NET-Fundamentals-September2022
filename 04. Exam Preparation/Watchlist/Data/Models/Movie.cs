namespace Watchlist.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Watchlist.Common;

    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.MovieTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.MovieDirectorMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        public int ImageUrl { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; } = null!;

        public List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
