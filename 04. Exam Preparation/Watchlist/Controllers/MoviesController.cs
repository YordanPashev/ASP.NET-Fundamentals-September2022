namespace Watchlist.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Watchlist.Models;
    using Watchlist.Services.Contracts;

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            MovieViewModel[] model = await movieService.GetAllMoviesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string userErrorMessage)
        {
            if (userErrorMessage != null)
            {
                ViewBag.UserErrorMessage = userErrorMessage;
            }

            AddMovieViewModel model = new AddMovieViewModel()
            {
                Genres = await movieService.GetAllGenresAsync()
            };

            model.Genres = model.Genres.OrderBy(g => g.Name).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            if (!movieService.IsGenreValid(model.GenreId))
            {
                return this.RedirectToAction("Add", "Movies", new { userErrorMessage = "Invalid Genre!" });
            }

            try
            {
                await movieService.AddMovieAsync(model);

                return this.RedirectToAction("All", "Movies");
            }

            catch (Exception)
            {
                return this.RedirectToAction("Add", "Movies", new { userErrorMessage = "Invalid data! Please Try again." });
            }
        }
    }
}
