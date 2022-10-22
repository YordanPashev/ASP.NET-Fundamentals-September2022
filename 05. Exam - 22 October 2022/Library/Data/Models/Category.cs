namespace Library.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Library.Common;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GlobalConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
