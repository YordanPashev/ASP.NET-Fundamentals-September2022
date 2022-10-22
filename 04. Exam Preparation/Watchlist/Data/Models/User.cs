namespace Watchlist.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public virtual List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
