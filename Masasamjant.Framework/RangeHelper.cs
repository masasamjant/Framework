namespace Masasamjant
{
    /// <summary>
    /// Helper to create <see cref="Range{T}"/> instances.
    /// </summary>
    public static class RangeHelper
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
}
