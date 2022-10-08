namespace ForumApp.Common
{
    public static class GlobalConstants
    {
        // Post

        //Title
        public const int PostTitleMinLength = 10;
        public const int PostTitleMaxLength = 50;
        
        //Content
        public const int PostContentMinLength = 30;
        public const int PostContentMaxLength = 1500;

        //User Error Messages
        public const string InvalidDataMessage = "Invalid data!";
        public const string NoPostFoundMessage = "No post found!";
        public const string PleaseMakeYourChangesMessage = "Please edit";
        public const string PostAlredyExist = "A post with the same data already exist!";

        // Successfull Operation Messages
        public const string SucessfulAddedPostMessage = "A new post have been added.";
        public const string SucessfulEditPostMessage = "The selected post have been edited.";
    }
}
