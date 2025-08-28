namespace Masasamjant.Web.ViewModels.Notifications
{
    /// <summary>
    /// Defines supported types of notification.
    /// </summary>
    public enum NotificationType : int
    {
        /// <summary>
        /// Information message that usually does not require user actions.
        /// Should be considered as neutral message for user.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Information message that tell user that performed action succeeded.
        /// Should be considered as positive message for user to continue.
        /// </summary>
        Success = 1,

        /// <summary>
        /// Information message that tell user that something is wrong in current input and requires user actions like if user enters invalid value.
        /// Should be considered as negative message for user to fix something in current action.
        /// </summary>
        /// <remarks>This should be considered as current user error.</remarks>
        Warning = 2,

        /// <summary>
        /// Error message that tell user that something not related to users current action is broken and because of that user action failed. 
        /// User cannot fix problem with current action. Should be considered as fatal message for user that requires fixing until current action can be completed.
        /// </summary>
        /// <remarks>This should be consirered as application error or other user error.</remarks>
        Error = 3,
    }
}
