namespace Library.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;

    using Library.Services.Contracts;
    using Library.Models.Book;

    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBooksService bookService;

        public BooksController(IBooksService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All(string? userErrorMessage = null)
        {
            if (userErrorMessage != null)
            {
                this.ViewBag.UserErrorMessage = userErrorMessage;
            }

            var model = await bookService.GetAllBooksAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string? userErrorMessage = null)
        {
            if (userErrorMessage != null)
            {
                this.ViewBag.UserErrorMessage = userErrorMessage;
            }

            AddBookViewModel model = new AddBookViewModel()
            {
                 Categories = await this.bookService.GetAllGategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            if (!bookService.IsCategoryExistInDb(model.CategoryId))
            {
                return this.RedirectToAction("Add", "Books", new { userErrorMessage = "Invalid Category!" });
            }

            try
            {
                await bookService.AddBookAsync(model);

                return this.RedirectToAction("All", "Books");
            }

            catch (Exception)
            {
                return this.RedirectToAction("Add", "Books", new { userErrorMessage = "Invalid data! Please Try again." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                string? userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.bookService.AddBookToTheUsersCollection(userId, bookId);
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("All", "Books", new { userErrorMessage = ex.Message });
            }

            return RedirectToAction("All", "Books");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await this.bookService.RemovebookFromusersCollectionAsync(userId, bookId);

            return RedirectToAction("Mine", "Books");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await this.bookService.GetAllUsersBooksAsync(userId);

            return this.View(model);
        }
    }
}
