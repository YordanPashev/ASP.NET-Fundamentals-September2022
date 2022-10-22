namespace Library.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Library.Data;
    using Library.Data.Models;
    using Library.Services.Contracts;
    using Library.Models.Book;

    public class BooksService : IBooksService
    {
        private readonly LibraryDbContext context;

        public BooksService(LibraryDbContext dbcontext)
        {
            this.context = dbcontext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToTheUsersCollection(string userId, int bookId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.ApplicationUsersBooks)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            Book? book = this.context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID.");
            }

            if(user.ApplicationUsersBooks.Any(ub => ub.BookId == bookId))
            {
                throw new ArgumentException("The Book already exist in your collection.");
            }

            user.ApplicationUsersBooks.Add(new ApplicationUserBook()
            {
                BookId = book.Id,
                ApplicationUserId = user.Id,
                Book = book,
                ApplicationUser = user
            });

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var books = await this.context.Books.Include(b => b.Category).ToArrayAsync();

            return books.Select(b => new BookViewModel()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating,
                CategoryName = b.Category.Name,
            });
        }

        public async Task<List<Category>> GetAllGategoriesAsync()
            => await context.Categories.ToListAsync();

        public bool IsCategoryExistInDb(int addModelCategoryId)
            =>  this.context.Categories.Any(c => c.Id == addModelCategoryId);

        public async Task RemovebookFromusersCollectionAsync(string userId, int bookId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var movie = user.ApplicationUsersBooks.FirstOrDefault(m => m.BookId == bookId);

            if (movie != null)
            {
                user.ApplicationUsersBooks.Remove(movie);

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<BookViewModel>> GetAllUsersBooksAsync(string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.ApplicationUsersBooks)
               .ThenInclude(u => u.Book)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            List<BookViewModel> usersBooks = new List<BookViewModel>();

            foreach (var item in user.ApplicationUsersBooks)
            {
                BookViewModel currBookToAdd = new BookViewModel()
                {
                    Id = item.Book.Id,
                    Title = item.Book.Title,
                    Author = item.Book.Author,
                    Description = item.Book.Description,
                    ImageUrl = item.Book.ImageUrl,
                    Rating = item.Book.Rating,
                };

                usersBooks.Add(currBookToAdd);
            }

            return usersBooks;
        }
    }
}
