namespace Watchlist.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Watchlist.Models;
    using Watchlist.Services.Contracts;

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
            => this.movieService = movieService;

        [HttpGet]
        public async Task<IActionResult> All(string? userMessage = null)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            MovieViewModel[] model = await this.movieService.GetAllMoviesAsync();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string userErrorMessage)
        {
            if (userErrorMessage != null)
            {
                this.ViewBag.UserErrorMessage = userErrorMessage;
            }

            AddMovieViewModel model = new AddMovieViewModel()
            {
                Genres = await this.movieService.GetAllGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.movieService.IsGenreExistInDb(model.GenreId))
            {
                return this.RedirectToAction("Add", "Movies", new { userErrorMessage = "Invalid Genre!" });
            }

            try
            {
                await this.movieService.AddMovieAsync(model);
                return this.RedirectToAction("All", "Movies");
            }
            catch (Exception)
            {
                return this.RedirectToAction("Add", "Movies", new { userErrorMessage = "Invalid data! Please Try again." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                string? currUserId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.movieService.AddMovieToTheUsersCollection(currUserId, movieId);
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("All", "Movies", new { userMessage = ex.Message });
            }

            string? addedMovieTitle = this.movieService.GetMovieTitleById(movieId);
            return this.RedirectToAction("All", "Movies", new { userMessage = $"You successfully added {addedMovieTitle} to your collection."});
        }

        [HttpGet]
        public async Task<IActionResult> Watched(string? userMessage = null)
        {
            if(userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            List<MovieViewModel> model = new List<MovieViewModel>();

            try
            {
                string? currUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                model = await this.movieService.GetAllUsersMoviesAsync(currUserId);
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("All", "Movies", new { userMessage = ex.Message });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            List<MovieViewModel> model = new List<MovieViewModel>();

            try
            {
                string? currUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.movieService.RemoveMovieFromUsersCollectionAsync(currUserId, movieId);

            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Watched", "Movies", new { userMessage = ex.Message });
            }

            string? removedMovieTitle = this.movieService.GetMovieTitleById(movieId);
            return this.RedirectToAction("Watched", "Movies", new { userMessage = $"You successfully removed {removedMovieTitle} from your collection." });
        }
    }
}
