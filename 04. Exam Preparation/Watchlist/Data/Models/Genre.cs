namespace Watchlist.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Watchlist.Common;

    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.GenreNameMaxLength)]
        public string Name { get; set; } = null!;

        public Movie Movie { get; set; } = null!;
    }
}
