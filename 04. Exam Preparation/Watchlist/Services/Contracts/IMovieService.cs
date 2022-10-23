namespace Watchlist.Services.Contracts
{
    using Watchlist.Data.Models;
    using Watchlist.Models;

    public interface IMovieService
    {
        Task AddMovieAsync(AddMovieViewModel model);

        Task AddMovieToTheUsersCollection(string? currUserId, int selectedMovieId);

        Task<List<Genre>> GetAllGenresAsync();

        Task<MovieViewModel[]> GetAllMoviesAsync();

        string? GetMovieTitleById(int movieId);

        Task<List<MovieViewModel>> GetAllUsersMoviesAsync(string? currUserId);

        bool IsGenreExistInDb(int genreId);

        Task RemoveMovieFromUsersCollectionAsync(string? currUserId, int selectedMovieId);
    }
}
