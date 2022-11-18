namespace TaskBoardApp.Common
{
    public class GlobalConstants
    {
        // Task
        public const int TaskTitleMinLength = 5;
        public const int TaskTitleMaxLength = 70;

        public const int TaskDescriptionMinLength = 10;
        public const int TaskDescriptionMaxLength = 1000;

        public const string TaskDateTimeFormat = "d MMMM yyyy, HH:mm";


        // User
        public const int UserFirstNameMinLength = 2;
        public const int UserFirstNameMaxLength = 15;

        public const int UserLastNameMinLength = 2;
        public const int UserLastNameMaxLength = 15;

        // Board
        public const int BoardNameMinLength = 3;
        public const int BoardNameMaxLength = 30;

        // User Messages
        public const string InvalidDataMessage = "Invalid data!";
        public const string InvalidBoardMessage = "Invalid board!";
        public const string NewBoardAddedMessage = "A new board has been added";
        public const string NewTaskAddedMessage = "A new task has been added";    
        public const string PleaseEditSelectedTaskUserMessage = "Please edit the selected task.";
        public const string SuccessfullyDeletedTaskMessage = "The task has been successfully deleted.";
        public const string SuccessfullyEditedTaskMessage = "The task has been successfully edited.";
    }
}
