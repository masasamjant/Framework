namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents a mouse input device.
    /// </summary>
    public interface IMouse : IInputDevice
    {
        /// <summary>
        /// Occurs when mouse move.
        /// </summary>
        event EventHandler<MouseEventArgs>? Move;

        /// <summary>
        /// Occurs when mouse button is pressed down.
        /// </summary>
        event EventHandler<MouseEventArgs>? ButtonPressed;

        /// <summary>
        /// Occurs when mouse button is released up.
        /// </summary>
        event EventHandler<MouseEventArgs>? ButtonReleased;

        /// <summary>
        /// Occurs when mouse button is released up.
        /// </summary>
        event EventHandler<MouseEventArgs>? ButtonClicked;

        /// <summary>
        /// Occurs when mouse button is double clicked.
        /// </summary>
        event EventHandler<MouseEventArgs>? ButtonDoubleClicked;

        /// <summary>
        /// Gets the mouse options.
        /// </summary>
        MouseOptions Options { get; }
    }
}
