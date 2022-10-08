namespace ForumApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using ForumApp.Data;
    using ForumApp.Models.Posts;
    using ForumApp.InfraStructures;
    using ForumApp.Common;
    using ForumApp.Data.Entities;

    public class PostsController : Controller
    {
        private readonly ForumAppDbContext context;
        private readonly PostsControllerExtension controllerExtension;

        public PostsController(ForumAppDbContext dbContext)
        {
            this.controllerExtension = new PostsControllerExtension();
            this.context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? userMessage = null)
        {
            PostViewModel[] allPosts = await GettAllActivePostsAsNoTracking();

            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            return View(allPosts);
        }

        [HttpGet]
        public IActionResult AddNewPost() => this.View();

        [HttpPost]
        public async Task<IActionResult> AddNewPost(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ViewBag.UserErrorMessage = GlobalConstants.InvalidDataMessage;
                return this.View();
            }

            return await this.controllerExtension.AddNewPostOrReturnErrorMessage(model, context);
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id, string? userMessage = null)
        {
            return await this.controllerExtension.ViewOrNoPostFound(id, context, userMessage);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                if (model?.Id == null)
                {
                    return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.NoPostFoundMessage });
                }

                return this.RedirectToAction("EditPost", "Posts", new { id = model.Id, userMessage = GlobalConstants.InvalidDataMessage });
            }

            return await this.controllerExtension.EditAndRedirectOrReturnNotFound(model, context);
        }

        private async Task<PostViewModel[]> GettAllActivePostsAsNoTracking()
        {
            return await context.Posts
                 .AsNoTracking()
                 .Select(p => new PostViewModel()
                 {
                     Id = p.Id,
                     Title = p.Title,
                     Content = p.Content,
                 })
                 .ToArrayAsync();
        }
    }
}
