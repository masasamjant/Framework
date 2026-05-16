namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Arguments for mouse-related events.
    /// </summary>
    public class MouseEventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="MouseEventArgs"/> class with the specified mouse location.
        /// </summary>
        /// <param name="location">The location of the mouse cursor.</param>
        public MouseEventArgs(MouseLocation location)
            : this(location, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="MouseEventArgs"/> class with the specified mouse location and buttons.
        /// </summary>
        /// <param name="location">The location of the mouse cursor.</param>
        /// <param name="buttons">The state of the mouse buttons.</param>
        public MouseEventArgs(MouseLocation location, MouseButtonInfo? button)
        {
            Location = location;
            Button = button;
        }

        /// <summary>
        /// Gets the location of the mouse cursor at the time the event was raised.
        /// </summary>
        public MouseLocation Location { get; }

        /// <summary>
        /// Gets the mouse button related to event.
        /// </summary>
        public MouseButtonInfo? Button { get; }

        /// <summary>
        /// Gets or sets if event was handled.
        /// </summary>
        public bool IsHandled { get; set; }
    }
}
