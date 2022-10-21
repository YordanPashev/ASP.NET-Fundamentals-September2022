namespace Watchlist.Services.Contracts
{
    using Watchlist.Data.Models;
    using Watchlist.Models;

    public interface IMovieService
    {
        Task<MovieViewModel[]> GetAllMoviesAsync();

        Task AddMovieAsync(AddMovieViewModel model);

        Task<List<Genre>> GetAllGenresAsync();

        bool IsGenreValid(int genreId);
    }
}
