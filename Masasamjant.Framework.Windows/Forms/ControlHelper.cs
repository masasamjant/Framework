namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Provides helper methods to controls in general.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// Relocates a control within a container based on the specified dimensions and offsets.
        /// </summary>
        /// <param name="control">The control to relocate.</param>
        /// <param name="containerWidth">The width of the container.</param>
        /// <param name="containerHeight">The height of the container.</param>
        /// <param name="deltaX">The horizontal offset.</param>
        /// <param name="deltaY">The vertical offset.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="control"/> is <c>null</c>.</exception>
        public static void Relocate(this Control control, int containerWidth, int containerHeight, int deltaX, int deltaY)
        {
            ArgumentNullException.ThrowIfNull(control);
            int x = containerWidth - deltaX;
            int y = containerHeight - deltaY;
            control.Location = new Point(x, y);
        }
    }
}
