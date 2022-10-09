namespace ForumApp.Common
{
    public static class GlobalConstants
    {
        // Post
        public const int PostTitleMinLength = 10;
        public const int PostTitleMaxLength = 50;
        
        public const int PostContentMinLength = 30;
        public const int PostContentMaxLength = 1500;


        // User Error Messages
        public const string InvalidDataMessage = "Invalid data!";
        public const string NoPostFoundMessage = "No post found!";
        public const string PleaseMakeYourChangesMessage = "Please edit";
        public const string PostAlredyExist = "A post with the same data already exist!";
        

        // Successfull Operation Messages
        public const string SucessfullyAddedPostMessage = "A new post have been added.";
        public const string SucessfullyEditPostMessage = "The selected post have been edited.";
        public const string SucessfullyDeletedPostMessage = "The selected post have been deleted.";


        // Date Time Format
        public const string DefaultDateTimeFormat = "d MMMM yyyy, HH:mm";
    }
}
