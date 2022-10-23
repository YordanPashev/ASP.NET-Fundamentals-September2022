namespace Watchlist.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using Watchlist.Data;
    using Watchlist.Data.Models;
    using Watchlist.Models;
    using Watchlist.Services.Contracts;

    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext dbcontext)
        {
            this.context = dbcontext;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            Movie movie = new Movie()
            {
                GenreId = model.GenreId,
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
            };
            
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public bool IsGenreValid(int genreId)
            => context.Genre.Any(g => g.Id == genreId);

        public async Task<List<Genre>> GetAllGenresAsync()
            => await context.Genre.ToListAsync();

        public async Task<MovieViewModel[]> GetAllMoviesAsync()
        {
            var allMovies = await context.Movies.Include(m => m.Genre).ToListAsync();

            return allMovies.Select(m => new MovieViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                ImageUrl = m.ImageUrl,
                Rating = m.Rating,
                Genre = m.Genre.Name             
            })
            .OrderBy(m => m.Title)
            .ToArray();
        }
    }
}
