namespace Masasamjant.Windows
{
    /// <summary>
    /// Represents the event data for a drive information event.
    /// </summary>
    public sealed class DriveInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveInfoEventArgs"/> class.
        /// </summary>
        /// <param name="drive">The <see cref="DriveInfo"/> associated with the event.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is null.</exception>
        public DriveInfoEventArgs(DriveInfo drive)
        {
            Drive = drive ?? throw new ArgumentNullException(nameof(drive));
        }

        /// <summary>
        /// Gets the <see cref="DriveInfo"/> associated with the event.
        /// </summary>
        public DriveInfo Drive { get; }
    }
}
