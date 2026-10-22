using System.Diagnostics;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// A <see cref="IOutput"/> that writes to the <see cref="Debug"/> output.
    /// </summary>
    public sealed class DebugOutput : IOutput
    {
        /// <summary>
        /// Writes specified value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(string value)
        {
            Debug.Write(value);
        }

        /// <summary>
        /// Writes specified value as line.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLine(string value)
        {
            Debug.WriteLine(value);
        }

        Task IOutput.WriteAsync(string value)
        {
            Write(value);
            return Task.CompletedTask;
        }


        Task IOutput.WriteLineAsync(string value)
        {
            WriteLine(value);
            return Task.CompletedTask;
        }
    }
}
