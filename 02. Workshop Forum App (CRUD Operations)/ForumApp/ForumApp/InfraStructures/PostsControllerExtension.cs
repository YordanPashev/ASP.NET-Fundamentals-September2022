namespace ForumApp.InfraStructures
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using ForumApp.Common;
    using ForumApp.Data;
    using ForumApp.Data.Entities;
    using ForumApp.Models.Posts;

    public class PostsControllerExtension : Controller
    {
        public async Task<IActionResult> AddNewPostOrReturnErrorMessage
            (PostViewModel model, ForumAppDbContext context)
        {
            if (PostAlreadyExistInDb(model, context))
            {
                this.ViewBag.UserMessage = GlobalConstants.PostAlredyExist;
                return this.View(model);
            }

            await AddNewPostToDb(model, context);

            return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.SucessfulAddedPostMessage});
        }

        public async Task<IActionResult> EditAndRedirectOrReturnNotFound
            (PostViewModel model, ForumAppDbContext context)
        {
            Post? postToEdit = await context.Posts.FirstOrDefaultAsync(p => p.Id.Equals(model.Id));

            if (postToEdit == null)
            {
                return this.RedirectToAction("EditPost", "Posts", new { userMessage = GlobalConstants.NoPostFoundMessage });
            }

            if (postToEdit.Title == model.Title &&
                postToEdit.Content == model.Content)
            {
                return this.RedirectToAction("EditPost", "Posts", new { userMessage = GlobalConstants.PleaseMakeYourChangesMessage });
            }

            postToEdit.Title = model.Title;
            postToEdit.Content = model.Content;

            await context.SaveChangesAsync();

            return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.SucessfulEditPostMessage });
        }

        private bool PostAlreadyExistInDb(PostViewModel model, ForumAppDbContext context)
        {
            if (context.Posts.Any(p => p.Title == model.Title) &&
                context.Posts.Any(p => p.Content == model.Content))
            {
                return true;
            }

            return false;
        }

        private async Task AddNewPostToDb(PostViewModel model, ForumAppDbContext context)
        {
            Post newPost = new Post()
            {
                Id = new Guid(),
                Title = model.Title,
                Content = model.Content
            };

            await context.Posts.AddAsync(newPost);
            await context.SaveChangesAsync();
        }

        public async Task<IActionResult> ViewOrNoPostFound(Guid? id, ForumAppDbContext context, string userMessage)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }

            Post? post = await context.Posts.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (post == null)
            {
                return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.NoPostFoundMessage });
            }

            PostViewModel model = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
            };

            return this.View(model);
        }
    }
}
