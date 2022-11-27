namespace TaskBoardAppTests.Common
{
    using Microsoft.EntityFrameworkCore;
    using System;

    using TaskBoardApp.Data;
    using TaskBoardApp.Data.Entities;
    using TaskBoardApp.Models;

    public static class HelperMethods
    {
        public static TaskBoardApp.Data.Entities.Board CreateBoard(string boardName)
            => new TaskBoardApp.Data.Entities.Board()
            {
                Id = Guid.NewGuid(),
                Name = "TODO"
            };

        public static TaskBoardDbContext CreateTaskBoardDbContextMock()
        {
            DbContextOptions<TaskBoardDbContext> options = new DbContextOptionsBuilder<TaskBoardDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            return new TaskBoardDbContext(options, true);
        }

        public static CreateTaskViewModel CreateNewTaskViewModel(string title, string description, Guid boardId, string ownerUserName)
            => new CreateTaskViewModel()
            {
                Title = title,
                Description = description,
                BoardId = boardId,
                OwnerUsername = ownerUserName
            };

        public static TaskBoardApp.Data.Entities.Task CreateTask(Board? board, string title, User user, string taskOneDescription)
            => new TaskBoardApp.Data.Entities.Task()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = "To clean the house on Sunday.",
                CreatedOn = DateTime.Now,
                BoardId = board.Id,
                OwnerId = user.Id,
            };

        public static async System.Threading.Tasks.Task<User> CreateUserWithNameKichka(TaskBoardDbContext dbContext)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Kichka",
                PasswordHash = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3ca",
                FirstName = "Kichka",
                LastName = "Tebeshirova",
                Email = "kichetu@abv.bg"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public static async System.Threading.Tasks.Task<User> CreateUserWithNameDimitrichko(TaskBoardDbContext dbContext)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Dimitrihcko",
                PasswordHash = "5994471abb01112afcc18159f6cc74b4f511b99806da59bsd4a",
                FirstName = "Dimitrichko",
                LastName = "Todorov",
                Email = "dimchou_t@abv.bg"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
