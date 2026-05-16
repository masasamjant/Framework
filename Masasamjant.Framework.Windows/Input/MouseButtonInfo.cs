using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents information about mouse button state.
    /// </summary>
    public readonly struct MouseButtonInfo : IEquatable<MouseButtonInfo>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="MouseButtonInfo"/> struct.
        /// </summary>
        /// <param name="button">The mouse button.</param>
        /// <param name="state">The state of the mouse button.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="button"/> or <paramref name="state"/> is not defined.</exception>
        public MouseButtonInfo(MouseButton button, MouseButtonState state)
        {
            if (!Enum.IsDefined(button))
                throw new ArgumentException("Invalid mouse button.", nameof(button));

            if (!Enum.IsDefined(state))
                throw new ArgumentException("Invalid mouse button state.", nameof(state));

            Button = button;
            State = state;
        }

        /// <summary>
        /// Gets the mouse button.
        /// </summary>
        public MouseButton Button { get; }

        /// <summary>
        /// Gets the button state.
        /// </summary>
        public MouseButtonState State { get; }

        /// <summary>
        /// Determines if other <see cref="MouseButtonInfo"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The other <see cref="MouseButtonInfo"/> to compare.</param>
        /// <returns><c>true</c> if the other <see cref="MouseButtonInfo"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(MouseButtonInfo other)
        {
            return Button == other.Button && State == other.State;
        }

        /// <summary>
        /// Determines if object instance is <see cref="MouseButtonInfo"/> and equal to this instance.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if the object instance is a <see cref="MouseButtonInfo"/> and equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is MouseButtonInfo other && Equals(other);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Button, State);
        }

        /// <summary>
        /// Gets the string presentation in format "Button: State", e.g. "Left: Pressed".
        /// </summary>
        /// <returns>The string representation of the mouse button info.</returns>
        public override string ToString()
        {
            return $"{Button}: {State}";
        }

        /// <summary>
        /// Operator to determine if left and right <see cref="MouseButtonInfo"/> are equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if the left and right values are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(MouseButtonInfo left, MouseButtonInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Operator to determine if left and right <see cref="MouseButtonInfo"/> are not equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if the left and right values are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(MouseButtonInfo left, MouseButtonInfo right)
        {
            return !(left == right);
        }
    }
}
