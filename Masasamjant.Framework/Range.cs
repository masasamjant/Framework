using System.Collections;

namespace Masasamjant
{
    /// <summary>
    /// Factory class to create <see cref="Range{T}"/> instances.
    /// </summary>
    public static class Range
    {
        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<byte> GetRange(byte min, byte max, byte step = 1)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<short> GetRange(short min, short max, short step = 1)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<ushort> GetRange(ushort min, ushort max, ushort step = 1)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<int> GetRange(int min, int max, int step = 1)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<uint> GetRange(uint min, uint max, uint step = 1U)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<long> GetRange(long min, long max, long step = 1L)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<ulong> GetRange(ulong min, ulong max, ulong step = 1UL)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<float> GetRange(float min, float max, float step = 1F)
        {
            return GetRange(min, max, step, (a, b) => a += b);
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<double> GetRange(double min, double max, double step = 1D)
        {
            int digits = NumberHelper.GetHighestDigits([min, max, step]);
            return GetRange(min, max, step, (a, b) => Math.Round(a += b, digits));
        }

        /// <summary>
        /// Gets range of values from <paramref name="min"/> to <paramref name="max"/> using specified step.
        /// </summary>
        /// <param name="min">The minimum range value.</param>
        /// <param name="max">The maximum range value.</param>
        /// <param name="step">The step. Default is 1.</param>
        /// <returns>A range of values.</returns>
        public static Range<decimal> GetRange(decimal min, decimal max, decimal step = 1M)
        {
            int digits = NumberHelper.GetHighestDigits([min, max, step]);
            return GetRange(min, max, step, (a, b) => Math.Round(a += b, digits));
        }

        private static Range<T> GetRange<T>(T min, T max, T step, Func<T, T, T> addStep) where T : IComparable<T>
        {
            T minimum = ComparableHelper.Min(min, max);
            T maximum = ComparableHelper.Max(min, max);
            return new Range<T>(minimum, maximum, step, addStep);
        }
    }

    /// <summary>
    /// Represents range of <typeparamref name="T"/> values.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public sealed class Range<T> : IEquatable<Range<T>>, ICloneable, IEnumerable<T> 
        where T : IComparable<T>
    {
        private readonly T min;
        private readonly T max;
        private readonly T step;
        private readonly Func<T, T, T> addStep;

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="step">The step.</param>
        /// <param name="addStep">The delegate to add step.</param>
        internal Range(T min, T max, T step, Func<T, T, T> addStep)
        {
            this.min = min;
            this.max = max;
            this.step = step;
            this.addStep = addStep;
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public T Min
        {
            get { return min; }
        }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        public T Max
        {
            get { return max; }
        }

        /// <summary>
        /// Gets the value change step.
        /// </summary>
        public T Step
        {
            get { return step; }
        }

        /// <summary>
        /// Gets the range of values.
        /// </summary>
        public IEnumerable<T> Values
        {
            get
            {
                T minimum = Min;
                T maximum = Max;

                if (minimum.IsEqual(maximum))
                    yield break;

                do
                {
                    T current = minimum;

                    minimum = addStep(minimum, step);

                    if (minimum.IsGreaterThan(maximum))
                        yield return maximum;
                    else
                        yield return current;
                }
                while (minimum.IsLessThanOrEqual(maximum));
            }
        }

        /// <summary>
        /// Check if other <see cref="Range{T}"/> is equal to this instance. Meaning they have same minimum, maximum, step and values.
        /// </summary>
        /// <param name="other">The other <see cref="Range{T}"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(Range<T>? other)
        {
            if (other != null && ComparableHelper.IsEqual(Max, other.Max) && ComparableHelper.IsEqual(Min, other.Min) && ComparableHelper.IsEqual(Step, other.Step))
            {
                return ArrayHelper.AreEqual(Values.ToArray(), other.Values.ToArray());
            }

            return false;
        }

        /// <summary>
        /// Check if object instance is <see cref="Range{T}"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="Range{T}"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Range<T>);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            int code = HashCode.Combine(Min, Max, Step);

            foreach (var value in Values)
            {
                code ^= value.GetHashCode();
            }

            return code;
        }

        /// <summary>
        /// Gets string representation of this instance where values are separated by space.
        /// </summary>
        /// <returns>A string representation.</returns>
        public override string ToString()
        {
            return ToString(' ');
        }

        /// <summary>
        /// Gets string representation of this instance where values are separated by specified separator.
        /// </summary>
        /// <param name="separator">The value separator.</param>
        /// <returns>A string representation.</returns>
        public string ToString(char separator)
        {
            return string.Join(separator, Values);
        }

        /// <summary>
        /// Creates copy from this range.
        /// </summary>
        /// <returns>A copy from this range.</returns>
        public Range<T> Clone()
        {
            return new Range<T>(min, max, step, addStep);
        }

        /// <summary>
        /// Gets enumerator to iterate range values.
        /// </summary>
        /// <returns>A enumerator to iterate range values.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in Values)
            {
                yield return value;
            }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
