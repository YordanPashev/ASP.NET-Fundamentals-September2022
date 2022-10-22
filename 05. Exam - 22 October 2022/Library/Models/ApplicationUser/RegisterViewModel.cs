namespace Library.Models.ApplicationUser
{
    using System.ComponentModel.DataAnnotations;

    using Library.Common;

    public class RegisterViewModel
    {
        [Required]
        [MinLength(GlobalConstants.UserNameMinLength)]
        [MaxLength(GlobalConstants.UserNameMaxLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MinLength(GlobalConstants.EmailMinLength)]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.PasswordMinLength)]
        [MaxLength(GlobalConstants.PasswordMaxLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.PasswordMinLength)]
        [MaxLength(GlobalConstants.PasswordMaxLength)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
