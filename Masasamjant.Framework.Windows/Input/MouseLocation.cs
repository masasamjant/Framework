using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents the location of the mouse cursor on the screen.
    /// </summary>
    public readonly struct MouseLocation : IEquatable<MouseLocation>
    {
        /// <summary>
        /// Initializes new <see cref="MouseLocation"/> with specified coordinates.
        /// </summary>
        /// <param name="x">The X-coordinate.</param>
        /// <param name="y">The Y-coordinate.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="x"/> or <paramref name="y"/> is less than 0.</exception>
        public MouseLocation(int x, int y)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException(nameof(x), x, "X coordinate cannot be negative.");

            if (y < 0)
                throw new ArgumentOutOfRangeException(nameof(y), y, "Y coordinate cannot be negative.");

            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the X coordinate of the mouse cursor.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y coordinate of the mouse cursor.
        /// </summary>
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Determines whether the current instance and the specified <see cref="MouseLocation"/> represent the same coordinates.
        /// </summary>
        /// <param name="other">The <see cref="MouseLocation"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the current instance and the specified <see cref="MouseLocation"/> have the same X and Y values; otherwise, <c>false</c>.</returns>
        public bool Equals(MouseLocation other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="MouseLocation"/> instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="MouseLocation"/> instance.</param>
        /// <returns><c>true</c> if the specified object is a <see cref="MouseLocation"/> and is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is MouseLocation other && Equals(other);
        }

        /// <summary>
        /// Gets the hash code for the current <see cref="MouseLocation"/> instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return X ^ Y;
        }

        /// <summary>
        /// Gets string presentation in format [X,Y].
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        /// <summary>
        /// Operator to determine whether two <see cref="MouseLocation"/> instances represent the same coordinates.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if the left and right values represent the same coordinates; otherwise, <c>false</c>.</returns>
        public static bool operator ==(MouseLocation left, MouseLocation right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Operator to determine whether two <see cref="MouseLocation"/> instances represent different coordinates.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if the left and right values represent different coordinates; otherwise, <c>false</c>.</returns>
        public static bool operator !=(MouseLocation left, MouseLocation right) 
        {
            return !(left == right);
        }
    }
}
