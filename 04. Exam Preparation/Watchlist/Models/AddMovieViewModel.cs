namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    using Watchlist.Common;
    using Watchlist.Data.Models;

    public class AddMovieViewModel
    {
        [Required]
        [MinLength(GlobalConstants.MovieTitleMinLength)]
        [MaxLength(GlobalConstants.MovieTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.MovieDirectorMinLength)]
        [MaxLength(GlobalConstants.MovieDirectorMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), GlobalConstants.MovieRaitingMinValue, GlobalConstants.MovieRaitingMaxValue)]
        public decimal Rating { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
