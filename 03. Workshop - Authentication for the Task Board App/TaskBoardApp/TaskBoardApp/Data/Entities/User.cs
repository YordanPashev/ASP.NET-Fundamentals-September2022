namespace TaskBoardApp.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class User : IdentityUser
    {
        [Required]
        [StringLength(GlobalConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(GlobalConstants.UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;
    }
}
