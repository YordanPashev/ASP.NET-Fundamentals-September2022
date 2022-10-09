namespace ForumApp.Data.Configure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;

    using ForumApp.Data.Entities;

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            List<Post> initialPosts = GetInitialPosts();
            builder.HasData(initialPosts);
        }

        private List<Post> GetInitialPosts()
        {
            string filePath = "Data/InitialData/InitialPosts.json";
            string json = File.ReadAllText(filePath);

            Post[]? initialPosts = JsonConvert.DeserializeObject<Post[]>(json);

            return initialPosts.ToList();
        }
    }
}
