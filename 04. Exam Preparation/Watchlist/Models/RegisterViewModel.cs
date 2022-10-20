namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations;

    using Watchlist.Common;

    public class RegisterViewModel
    {
        [Required]
        [MinLength(GlobalConstants.UserNameMinLength)]
        [MaxLength(GlobalConstants.UserNameMaxLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(GlobalConstants.EmailMinLength)]
        [MaxLength(GlobalConstants.EmailMaxLength)]
        [EmailAddress]
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
