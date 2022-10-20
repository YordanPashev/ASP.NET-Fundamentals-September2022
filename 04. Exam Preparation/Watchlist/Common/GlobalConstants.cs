namespace Watchlist.Common
{
    public static class GlobalConstants
    {
        // User
        public const int UserNameMinLength = 5;
        public const int UserNameMaxLength = 20;

        public const int EmailMinLength = 10;
        public const int EmailMaxLength = 60;

        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;

        // Movie
        public const int MovieTitleMinLength = 10;
        public const int MovieTitleMaxLength = 50;

        public const int MovieDirectorMinLength = 5;
        public const int MovieDirectorMaxLength = 50;

        public const decimal MovieRaitingMinValue = 5;
        public const decimal MovieRaitingMaxValue = 20;

        // Genre
        public const int GenreNameMinLength = 5;
        public const int GenreNameMaxLength = 50;
    }
}
