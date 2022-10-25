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
            
            await this.context.Movies.AddAsync(movie);
            await this.context.SaveChangesAsync();
        }

        public async Task AddMovieToTheUsersCollection(string? currUserId, int selectedMovieId)
        {
            User? user = await this.context.Users
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync(u => u.Id == currUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID!");
            }

            Movie? movie = await this.context.Movies
                .FirstOrDefaultAsync(m => m.Id == selectedMovieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID!");
            }

            if (user.UsersMovies.Any(um => um.MovieId == selectedMovieId))
            {
                throw new ArgumentException("The Movie already exist in your Watchlist!");
            }

            user.UsersMovies.Add(new UserMovie()
            {
                UserId = currUserId,
                User = user,
                MovieId = selectedMovieId,
                Movie = movie
            });

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Genre>> GetAllGenresAsync()
            => await this.context.Genre.OrderBy(g => g.Name).ToListAsync();

        public async Task<MovieViewModel[]> GetAllMoviesAsync()
        {
            var allMovies = await this.context.Movies.Include(m => m.Genre).ToListAsync();

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

        public async Task<List<MovieViewModel>> GetAllUsersMoviesAsync(string? currUserId)
        {
            User? user = await this.context.Users
                .Include(u => u.UsersMovies)
                .ThenInclude(u => u.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(u => u.Id == currUserId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID!");
            }

            List<MovieViewModel> usersWatchlist = new List<MovieViewModel>();

            foreach (var um in user.UsersMovies)
            {
                MovieViewModel movie = new MovieViewModel()
                {
                    Id = um.Movie.Id,
                    Title = um.Movie.Title,
                    Director = um.Movie.Director,
                    ImageUrl = um.Movie.ImageUrl,
                    Rating = um.Movie.Rating,
                    Genre = um.Movie.Genre.Name
                };

                usersWatchlist.Add(movie);
            }

            return usersWatchlist.OrderBy(m => m.Title).ToList();
        }

        public string? GetMovieTitleById(int movieId)
            => this.context.Movies?
            .FirstOrDefault(m => m.Id == movieId)?.Title;
            
        public bool IsGenreExistInDb(int genreId)
            => this.context.Genre.Any(g => g.Id == genreId);

        public async Task RemoveMovieFromUsersCollectionAsync(string? currUserId, int selectedMovieId)
        {
            User? user = await this.context.Users
                .Where(u => u.Id == currUserId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            UserMovie? movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == selectedMovieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID!");
            }

            user.UsersMovies.Remove(movie);
            await this.context.SaveChangesAsync();
        }
    }
}
