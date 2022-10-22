namespace Library.Common
{
    public static class GlobalConstants
    {
        // ApplicationUser
        public const int UserNameMinLength = 5;
        public const int UserNameMaxLength = 20;

        public const int EmailMinLength = 10;
        public const int EmailMaxLength = 60;

        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;

        // Book
        public const int BookTitleMinLength = 10;
        public const int BookTitleMaxLength = 50;

        public const int BookAuthorMinLength = 5;
        public const int BookAuthorMaxLength = 50;

        public const int BookDescriptionMinLength = 5;
        public const int BookDescriptionMaxLength = 5000;

        public const string BookRaitingMinValue = "0.00";
        public const string BookRaitingMaxValue = "10.00";

        // Category
        public const int CategoryNameMinLength = 5;
        public const int CategoryNameMaxLength = 50;
    }
}
