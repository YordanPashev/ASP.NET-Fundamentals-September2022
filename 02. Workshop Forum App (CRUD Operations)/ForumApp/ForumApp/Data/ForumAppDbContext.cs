﻿namespace ForumApp.Data
{
    using Microsoft.EntityFrameworkCore;

    using ForumApp.Data.Entities;
    using ForumApp.Data.Configure;

    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
            :base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Post>(new PostConfiguration()); 
            builder.Entity<Post>();

            base.OnModelCreating(builder);
        }
    }
}
