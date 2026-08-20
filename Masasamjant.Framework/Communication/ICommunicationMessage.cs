namespace Masasamjant.Communication
{
    /// <summary>
    /// Represents a interface for a communication message that can be sent between communication parties like email, sms, etc.
    /// </summary>
    public interface ICommunicationMessage
    {
        /// <summary>
        /// Gets the unique identifier of the message. 
        /// This identifier can be used to track the message and its delivery status, and can also be used to correlate responses or acknowledgments with the original message.
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        string MessageType { get; }

        /// <summary>
        /// Gets the endpoint of the receiver of the message. 
        /// The endpoint can be an email address, phone number, or any other identifier that can be used to send the message to the intended recipient.
        /// </summary>
        string ReceiverEndpoint { get; }
        
        /// <summary>
        /// Gets the endpoint of the sender of the message. 
        /// The endpoint can be an email address, phone number, or any other identifier that can be used to identify the sender of the message.
        /// </summary>
        string? SenderEndpoint { get; }

        /// <summary>
        /// Gets the name of the sender of the message.
        /// </summary>
        string? SenderName { get; }

        /// <summary>
        /// Gets the body of the message.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// Gets the title of the message like subject for email.
        /// </summary>
        /// <remarks>If messaging system does not support titles, then this can be ignored.</remarks>
        string Title { get; }

        /// <summary>
        /// Gets the encoding of the message.
        /// </summary>
        /// <remarks>If not valid or available encoding, then can use whatever is default encoding.</remarks>
        string? MessageEncoding { get; }
    }
}
