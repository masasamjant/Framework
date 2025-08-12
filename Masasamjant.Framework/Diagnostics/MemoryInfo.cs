using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents information about memory usage.
    /// </summary>
    [TypeConverter(typeof(MemoryInfoConverter))]
    public readonly struct MemoryInfo
    {
        /// <summary>
        /// Initializes new value of the <see cref="MemoryInfo"/> structure.
        /// </summary>
        /// <param name="startBytes">The start bytes.</param>
        /// <param name="endBytes">The end bytes.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="startBytes"/> or value of <paramref name="endBytes"/> is less than 0.</exception>
        public MemoryInfo(long startBytes, long endBytes)
        {
            if (startBytes < 0)
                throw new ArgumentOutOfRangeException(nameof(startBytes), startBytes, "The start bytes must be greater than or equal to 0.");

            if (endBytes < 0)
                throw new ArgumentOutOfRangeException(nameof(endBytes), endBytes, "The end bytes must be greater than or equal to 0.");

            if (endBytes < startBytes)
                ObjectHelper.Swap(ref startBytes, ref endBytes);

            StartBytes = startBytes;
            EndBytes = endBytes;
        }

        /// <summary>
        /// Gets the start bytes.
        /// </summary>
        public long StartBytes { get; }

        /// <summary>
        /// Gets the end bytes.
        /// </summary>
        public long EndBytes { get; }

        /// <summary>
        /// Gets the difference as total bytes.
        /// </summary>
        public long TotalBytes
        {
            get { return EndBytes - StartBytes; }
        }

        /// <summary>
        /// Check if object instance is <see cref="MemoryInfo"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="MemoryInfo"/> and has same start and end bytes with this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is MemoryInfo other)
                return StartBytes == other.StartBytes && EndBytes == other.EndBytes;

            return false;
        }

        /// <summary>
        /// Gets hash code for this value.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return StartBytes.GetHashCode() ^ EndBytes.GetHashCode();
        }

        /// <summary>
        /// Gets string presentation using <see cref="DefaultMemoryInfoFormatter"/>.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            return ToString(new DefaultMemoryInfoFormatter());
        }

        /// <summary>
        /// Gets string presentatio using specified <see cref="IMemoryInfoFormatter"/>.
        /// </summary>
        /// <param name="formatter">The <see cref="IMemoryInfoFormatter"/>.</param>
        /// <returns>A string presentation.</returns>
        public string ToString(IMemoryInfoFormatter formatter)
        {
            var end = formatter.FormatByteCount(EndBytes);
            var total = formatter.FormatByteCount(TotalBytes);
            return $"{end} Total (+ {total})";
        }

        /// <summary>
        /// Check if left value is equal to right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(MemoryInfo left, MemoryInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Check if left value is not equal to right value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(MemoryInfo left, MemoryInfo right)
        {
            return !(left == right);
        }
    }
}
