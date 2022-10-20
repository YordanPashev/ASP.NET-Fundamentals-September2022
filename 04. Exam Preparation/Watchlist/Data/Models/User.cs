namespace Watchlist.Data
{
    using Microsoft.AspNetCore.Identity;

    using Watchlist.Data.Models;

    public class User : IdentityUser
    {
        public List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
