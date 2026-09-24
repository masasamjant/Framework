using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Applications
{
    /// <summary>
    /// Callback to invoke for each argument read.
    /// </summary>
    /// <param name="name">The name of the argument.</param>
    /// <param name="value">The value of the argument.</param>
    /// <returns><c>true</c> if the argument should be processed; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentException">If the argument value is invalid.</exception>
    public delegate bool ArgumentReadCallback(string name, string value);

    /// <summary>
    /// Represents reader of command line arguments formatted as <c>-name value</c>, <c>--name value</c>, <c>-name</c> or <c>--name</c>.
    /// </summary>
    /// <example>Value formatted like <code>-name value</code> or <code>--name value</code> is read as name-value pair, where name is <code>name</code> and value is <code>value</code>.</example>
    public sealed class ArgumentsReader
    {
        private readonly Dictionary<string, string> arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Reads the specified command line arguments and returns an instance of <see cref="ArgumentsReader"/> containing the parsed arguments.
        /// </summary>
        /// <param name="args">The input arguments.</param>
        /// <param name="readCallback">The callback to be invoked for each argument read.</param>
        /// <returns>An instance of <see cref="ArgumentsReader"/> containing the parsed arguments.</returns>
        /// <exception cref="ArgumentException">If the argument value is invalid.</exception>
        public static ArgumentsReader Read(string[] args, ArgumentReadCallback? readCallback = null)
        {
            return new ArgumentsReader(args, readCallback);
        }

        /// <summary>
        /// Get the value of the argument with the specified name, or the default value if the argument does not exist.
        /// </summary>
        /// <param name="name">The name of the argument to get the value for.</param>
        /// <param name="defaultValue">The default value to return if the argument does not exist.</param>
        /// <returns>The value of the argument if it exists; otherwise, the default value.</returns>
        public string GetValue(string name, string defaultValue = "")
        {
            if (arguments.TryGetValue(name, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Try to get the value of the argument with the specified name.
        /// </summary>
        /// <param name="name">The name of the argument to get the value for.</param>
        /// <param name="value">When this method returns, contains the value of the argument if it exists; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the argument with the specified name exists; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(string name, [NotNullWhen(true)] out string? value)
        {
            return arguments.TryGetValue(name, out value);
        }

        /// <summary>
        /// Check if contains argument with specified name.
        /// </summary>
        /// <param name="name">The name of the argument to check.</param>
        /// <returns><c>true</c> if the argument with the specified name exists; otherwise, <c>false</c>.</returns>
        public bool Contains(string name)
        {
            return arguments.ContainsKey(name);
        }

        private ArgumentsReader(string[] args, ArgumentReadCallback? readCallback)
        {
            Parse(args, readCallback); 
        }

        private void Parse(string[] args, ArgumentReadCallback? readCallback)
        {
            if (args.Length == 0)
                return;

            for (int index = 0; index < args.Length; index++)
            {
                if (args[index].StartsWith('-'))
                {
                    var name = args[index].TrimStart('-');
                    var value = string.Empty;

                    if (index + 1 < args.Length && !args[index + 1].StartsWith('-'))
                    {
                        value = args[index + 1];
                        index++;
                    }

                    if (readCallback != null && !readCallback(name, value))
                        continue;

                    arguments[name] = value;
                }
            }
        }
    }
}
