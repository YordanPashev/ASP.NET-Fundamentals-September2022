namespace Watchlist.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserMovie
    {
        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; } = null!;

        [Required]
        public User User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(MovieId))]
        public int MovieId { get; set; } 

        [Required]
        public Movie Movie { get; set; } = null!;
    }
}
