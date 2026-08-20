namespace Masasamjant.Communication
{
    /// <summary>
    /// Represents an exception that is thrown when an error occurs while dispatching a communication message.
    /// </summary>
    public class CommunicationMessageException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="CommunicationMessageException"/> class.
        /// </summary>
        /// <param name="communicationMessage">The communication message that caused the exception.</param>
        public CommunicationMessageException(ICommunicationMessage communicationMessage)
            : this(communicationMessage, "An error occurred while dispatching the communication message.", null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="CommunicationMessageException"/> class.
        /// </summary>
        /// <param name="communicationMessage">The communication message that caused the exception.</param>
        /// <param name="exceptionMessage">The message that describes the error.</param>
        public CommunicationMessageException(ICommunicationMessage communicationMessage, string exceptionMessage)
            : this(communicationMessage, exceptionMessage, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="CommunicationMessageException"/> class.
        /// </summary>
        /// <param name="communicationMessage">The communication message that caused the exception.</param>
        /// <param name="exceptionMessage">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception or <c>null</c>.</param>
        public CommunicationMessageException(ICommunicationMessage communicationMessage, string exceptionMessage, Exception? innerException)
            : base(exceptionMessage, innerException)
        {
            CommunicationMessage = communicationMessage;
        }

        /// <summary>
        /// Gets the communication message that caused the exception. 
        /// This property provides access to the message that was being dispatched when the exception occurred, allowing for further inspection or logging of the message details.
        /// </summary>
        public ICommunicationMessage CommunicationMessage { get; }
    }
}
