namespace Library.Services.Contracts
{
    using Library.Data.Models;
    using Library.Models.Book;

    public interface  IBooksService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();

        Task<List<Category>> GetAllGategoriesAsync();

        bool IsCategoryExistInDb(int addModelCategoryId);

        Task AddBookAsync(AddBookViewModel model);

        Task AddBookToTheUsersCollection(string userId, int bookId);

        Task RemovebookFromusersCollectionAsync(string userId, int bookId);

        Task<List<BookViewModel>> GetAllUsersBooksAsync(string userId);
    }
}
