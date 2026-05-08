namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents presentation command.
    /// </summary>
    public class PresentationCommand : IPresentationCommand
    {
        private bool enabled;

        /// <summary>
        /// Notifies when command is executed.
        /// </summary>
        public event EventHandler<PresentationCommandEventArgs>? Executed;

        /// <summary>
        /// Notifies when <see cref="IsEnabled"/> has changed.
        /// </summary>
        public event EventHandler? IsEnabledChanged;

        /// <summary>
        /// Initializes new instance of the <see cref="PresentationCommandEventArgs"/> class.
        /// </summary>
        /// <param name="name">The unique command name.</param>
        /// <param name="enabled"><c>true</c>, default, if enabled; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <c>null</c>, empty or only whitespace.</exception>
        public PresentationCommand(string name, bool enabled = true)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The command name is null, empty or only whitespace.");

            Name = name;
            IsEnabled = enabled;
        }

        /// <summary>
        /// Gets the unique name of the command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a value indicating whether command is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Executes command if enabled.
        /// </summary>
        /// <param name="original">The arguments of original event.</param>
        public virtual void Execute(EventArgs original)
        {
            if (!IsEnabled)
                return;
        
            var commandEventArgs = new PresentationCommandEventArgs(original);
            Executed?.Invoke(this, commandEventArgs);
        }

        /// <summary>
        /// Raises <see cref="IsEnabled"/> event. If override, then make sure to call base method to raise the event.
        /// </summary>
        protected virtual void OnEnabledChanged()
        {
            IsEnabledChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Represents presentation command.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of arguments of original event.</typeparam>
    public class PresentationCommand<TEventArgs> : PresentationCommand, IPresentationCommand<TEventArgs>
        where TEventArgs : EventArgs
    {
        /// <summary>
        /// Notifies when command is executed.
        /// </summary>
        public new event EventHandler<PresentationCommandEventArgs<TEventArgs>>? Executed;

        /// <summary>
        /// Initializes new instance of the <see cref="PresentationCommandEventArgs{TEventArgs}"/> class.
        /// </summary>
        /// <param name="name">The unique command name.</param>
        /// <param name="enabled"><c>true</c>, default, if enabled; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <c>null</c>, empty or only whitespace.</exception>
        public PresentationCommand(string name, bool enabled = true)
            : base(name, enabled)
        { }

        /// <summary>
        /// Executes command if enabled.
        /// </summary>
        /// <param name="original">The arguments of original event.</param>
        public void Execute(TEventArgs original)
        {
            if (!IsEnabled)
                return;

            var commandEventArgs = new PresentationCommandEventArgs<TEventArgs>(original);
            Executed?.Invoke(this, commandEventArgs);
        }

        /// <summary>
        /// Executes command if enabled.
        /// </summary>
        /// <param name="original">The arguments of original event.</param>
        /// <exception cref="NotSupportedException">If <paramref name="original"/> is not <typeparamref name="TEventArgs"/> event args.</exception>
        public override void Execute(EventArgs original)
        {
            if (!IsEnabled)
                return;

            if (original is TEventArgs typedOriginal)
            {
                Execute(typedOriginal);
            }
            else
            {
                throw new NotSupportedException($"The event arguments of type {original.GetType()} are not supported. Expected type is {typeof(TEventArgs)}.");
            }
        }
    }
}
