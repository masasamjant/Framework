namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Defines options for mouse events.
    /// </summary>
    public sealed class MouseOptions
    {
        private int moveTreshold = 0;

        /// <summary>
        /// Gets or sets the move threshold in pixels. 
        /// A mouse move event will only be raised when the mouse is moved more than the specified threshold from the previous location. 
        /// The default value is 0, which means that every mouse move message will raise a mouse move event.
        /// </summary>
        public int MoveThreshold
        {
            get => moveTreshold;
            set => moveTreshold = Math.Max(value, 0);
        }

        /// <summary>
        /// Gets or sets a value indicating whether mouse move events are suspended.
        /// </summary>
        public bool IsMoveSuspended { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether mouse click events are suspended.
        /// </summary>
        public bool IsClickSuspended { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether mouse double-click events are suspended.
        /// </summary>
        public bool IsDoubleClickSuspended { get; set; } = false;
    }
}
