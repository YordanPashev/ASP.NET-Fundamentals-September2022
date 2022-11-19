namespace TaskBoardApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Data.Entities;

    public class TaskBoardDbContext : IdentityDbContext<User>
    {
        public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options, bool IsInMemory = false)
            : base(options)
        {
            if (!IsInMemory)
            {
                this.Database.Migrate();
            }
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}