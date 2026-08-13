namespace Masasamjant.Web.Mvc.Notifications
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
        /// <example>Message that guides how to fill form.</example>
        Information = 0,

        /// <summary>
        /// Information message that tells user that performed action succeeded.
        /// Should be considered as positive message for user to continue.
        /// </summary>
        /// <example>Message that confirms successful form submission.</example>
        Success = 1,

        /// <summary>
        /// Information message that tells user that something is wrong in current input and requires user actions like if user enters invalid value.
        /// Should be considered as negative message for user to correct input.
        /// </summary>
        /// <remarks>This should be considered as current user error.</remarks>
        /// <example>User has entered an invalid value in a form field.</example>
        Warning = 2,

        /// <summary>
        /// Error message that tells user that something not related to user's current action is broken and because of that user action failed. 
        /// User cannot fix problem with current action. Should be considered as fatal message for user that requires fixing until current action can be completed.
        /// </summary>
        /// <remarks>Should be considered as critical message for user to be aware of system issues.</remarks>
        /// <example>Like when connection to external system is down and user action requires that connection.</example>
        Error = 3
    }
}
