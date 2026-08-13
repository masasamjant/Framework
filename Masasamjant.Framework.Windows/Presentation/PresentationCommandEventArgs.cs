namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Arguments of <see cref="IPresentationCommand.Executed"/> event.
    /// </summary>
    public class PresentationCommandEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PresentationCommand"/> event args.
        /// </summary>
        /// <param name="original">The arguments of the original event.</param>
        /// <param name="command">The command that was executed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="original"/> is <c>null</c> or <paramref name="command"/> is <c>null</c>.</exception>
        public PresentationCommandEventArgs(EventArgs original, IPresentationCommand command)
        {
            ArgumentNullException.ThrowIfNull(original);
            ArgumentNullException.ThrowIfNull(command);
            Original = original;
            Command = command;
        }

        /// <summary>
        /// Gets the arguments of the original event.
        /// </summary>
        public EventArgs Original { get; }

        /// <summary>
        /// Gets the command that was executed.
        /// </summary>
        public IPresentationCommand Command { get; }
    }

    /// <summary>
    /// Arguments of <see cref="IPresentationCommand{TEventArgs}"/> event.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the arguments of the original event.</typeparam>
    public class PresentationCommandEventArgs<TEventArgs> : PresentationCommandEventArgs
        where TEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PresentationCommand{TEventArgs}"/> event args.
        /// </summary>
        /// <param name="original">The arguments of the original event.</param>
        /// <param name="command">The command that was executed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="original"/> is <c>null</c> or <paramref name="command"/> is <c>null</c>.</exception>
        public PresentationCommandEventArgs(TEventArgs original, IPresentationCommand<TEventArgs> command)
            : base(original, command)
        { }

        /// <summary>
        /// Gets the arguments of the original event.
        /// </summary>
        public new TEventArgs Original
        {
            get { return (TEventArgs)base.Original; }
        }

        /// <summary>
        /// Gets the command that was executed.
        /// </summary>
        public new IPresentationCommand<TEventArgs> Command
        {
            get { return (IPresentationCommand<TEventArgs>)base.Command; }
        }
    }
}
