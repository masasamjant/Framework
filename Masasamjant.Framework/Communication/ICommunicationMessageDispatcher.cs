namespace Masasamjant.Communication
{
    /// <summary>
    /// Represents a dispatcher that is responsible for sending communication messages to their intended recipients.
    /// </summary>
    public interface ICommunicationMessageDispatcher
    {
        /// <summary>
        /// Dispatches the specified message for delivery to the intended recipient. 
        /// The implementation of this method should handle the actual sending of the message, whether it be through email, SMS, or any other communication channel. 
        /// The delivery of the message may happen immediately or may be queued for later delivery, depending on the implementation and the communication channel being used.
        /// </summary>
        /// <param name="message">The message to be dispatched.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="message"/> is not supported.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="message"/> is in a state that it cannot be dispatched.</exception>
        /// <exception cref="CommunicationMessageException">If dispatching <paramref name="message"/> fails.</exception>
        void DispatchMessage(ICommunicationMessage message);

        /// <summary>
        /// Dispatches the specified message for delivery to the intended recipient asynchronously.
        /// The implementation of this method should handle the actual sending of the message, whether it be through email, SMS, or any other communication channel. 
        /// The delivery of the message may happen immediately or may be queued for later delivery, depending on the implementation and the communication channel being used.
        /// </summary>
        /// <param name="message">The message to be dispatched.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="message"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="message"/> is not supported.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="message"/> is in a state that it cannot be dispatched.</exception>
        /// <exception cref="CommunicationMessageException">If dispatching <paramref name="message"/> fails.</exception>
        Task DispatchMessageAsync(ICommunicationMessage message, CancellationToken cancellationToken = default);
    }
}
