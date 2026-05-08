namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents presentation command.
    /// </summary>
    public interface IPresentationCommand
    {
        /// <summary>
        /// Notifies when command is executed.
        /// </summary>
        event EventHandler<PresentationCommandEventArgs>? Executed;

        /// <summary>
        /// Notifies when <see cref="IsEnabled"/> has changed.
        /// </summary>
        event EventHandler IsEnabledChanged;

        /// <summary>
        /// Gets the unique name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether command is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Executes command if enabled.
        /// </summary>
        /// <param name="original">The arguments of original event.</param>
        void Execute(EventArgs original);
    }

    /// <summary>
    /// Represents presentation command.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of arguments of original event.</typeparam>
    public interface IPresentationCommand<TEventArgs> : IPresentationCommand
        where TEventArgs : EventArgs
    {
        /// <summary>
        /// Notifies when command is executed.
        /// </summary>
        new event EventHandler<PresentationCommandEventArgs<TEventArgs>>? Executed;
        
        /// <summary>
        /// Executes command if enabled.
        /// </summary>
        /// <param name="original">The arguments of original event.</param>
        void Execute(TEventArgs original);
    }
}
