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
            if (IsPostAlreadyExistInDb(model, context))
            {
                this.ViewBag.UserMessage = GlobalConstants.PostAlredyExist;
                return this.View(model);
            }

            await CreateNewPostAndAddItToDb(model, context);

            return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.SucessfullyAddedPostMessage});
        }

        public async Task<IActionResult> DeleteAndRedirectOrNotFound(Guid id, ForumAppDbContext context)
        {

            Post? postToEdit = await context.Posts.FirstOrDefaultAsync(p => p.Id.Equals(id) && p.IsDeleted == false);

            if (postToEdit == null)
            {
                return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.NoPostFoundMessage });
            }

            postToEdit.IsDeleted = true;
            postToEdit.DeletedOn = DateTime.Now;

            await context.SaveChangesAsync();
            return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.SucessfullyDeletedPostMessage });
        }

        public async Task<IActionResult> EditAndRedirectOrReturnNotFound
            (PostViewModel model, ForumAppDbContext context)
        {
            Post? postToEdit = await context.Posts.FirstOrDefaultAsync(p => p.Id.Equals(model.Id) && p.IsDeleted == false);

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
            postToEdit.EditedOn = DateTime.Now;

            await context.SaveChangesAsync();

            return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.SucessfullyEditPostMessage });
        }

        public async Task<IActionResult> ViewOrNoPostFound(Guid? id, ForumAppDbContext context, string? userMessage)
        {
            if (userMessage != null)
            {
                this.ViewBag.UserMessage = userMessage;
            }
 
            Post? post = await context.Posts.FirstOrDefaultAsync(p => p.Id.Equals(id) && p.IsDeleted == false);

            if (post == null)
            {
                return this.RedirectToAction("Index", "Posts", new { userMessage = GlobalConstants.NoPostFoundMessage });
            }

            PostViewModel model = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.CreatedOn
            };

            return this.View(model);
        }

        private async Task CreateNewPostAndAddItToDb(PostViewModel model, ForumAppDbContext context)
        {
            Post newPost = new Post()
            {
                Id = new Guid(),
                Title = model.Title,
                Content = model.Content,
                CreatedOn = DateTime.Now
            };

            await context.Posts.AddAsync(newPost);
            await context.SaveChangesAsync();
        }

        private bool IsPostAlreadyExistInDb(PostViewModel model, ForumAppDbContext context)
        {
            if (context.Posts.Any(p => p.Title == model.Title &&
                p.Content == model.Content && p.IsDeleted == false))
            {
                return true;
            }

            return false;
        }
    }
}
