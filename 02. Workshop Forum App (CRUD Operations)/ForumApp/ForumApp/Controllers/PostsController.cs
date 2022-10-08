using ForumApp.Data;
using ForumApp.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext context;

        public PostsController(ForumAppDbContext dbContext)
            => this.context = dbContext;

        public async Task<IActionResult> Index()
        {
            PostViewModel[] allPosts= await context.Posts
                 .Select(p => new PostViewModel()
                 {
                     Id = p.Id,
                     Title = p.Title,
                     Content = p.Content,
                 })
                 .ToArrayAsync();

            return View(allPosts);
        }
    }
}
