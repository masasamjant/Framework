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
        /// <exception cref="ArgumentNullException">If <paramref name="original"/> is <c>null</c>.</exception>
        public PresentationCommandEventArgs(EventArgs original)
        {
            ArgumentNullException.ThrowIfNull(original);
            Original = original;
        }

        /// <summary>
        /// Gets the arguments of the original event.
        /// </summary>
        public EventArgs Original { get; }
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
        /// <exception cref="ArgumentNullException">If <paramref name="original"/> is <c>null</c>.</exception>
        public PresentationCommandEventArgs(TEventArgs original)
            : base(original)
        { }

        /// <summary>
        /// Gets the arguments of the original event.
        /// </summary>
        public new TEventArgs Original
        {
            get { return (TEventArgs)base.Original; }
        }
    }
}
