using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Provides helper methods to work with <see cref="IPresentationCommands"/> collections.
    /// </summary>
    public static class PresentationCommandsHelper
    {
        /// <summary>
        /// Find <see cref="IPresentationCommand"/> with specified name.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to find.</param>
        /// <returns>A <see cref="IPresentationCommand"/> with the specified name, or <c>null</c> if not found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandName"/> is <c>null</c>, empty or only whitespace.</exception>
        public static IPresentationCommand? FindPresentationCommand(this IPresentationCommands commands, string commandName)
        {
            ArgumentNullException.ThrowIfNull(commands);

            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentNullException(nameof(commandName), "The command name is null, empty or only whitespace.");

            foreach (var command in commands)
            {
                if (command.Name == commandName)
                    return command;
            }

            return null;
        }

        /// <summary>
        /// Get <see cref="IPresentationCommand"/> with specified name. 
        /// Throws <see cref="InvalidOperationException"/> if command with specified name does not exist.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to find.</param>
        /// <returns>A <see cref="IPresentationCommand"/> with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandName"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If command does not exist.</exception>
        public static IPresentationCommand GetPresentationCommand(this IPresentationCommands commands, string commandName)
        {
            var command = FindPresentationCommand(commands, commandName);
            return command ?? throw new InvalidOperationException($"Command '{commandName}' does not exist.");
        }

        /// <summary>
        /// Tries to get <see cref="IPresentationCommand"/> with specified name.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to get.</param>
        /// <param name="command">The command if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the command exists; otherwise <c>false</c>.</returns>
        public static bool TryGetPresentationCommand(this IPresentationCommands commands, string commandName, [NotNullWhen(true)] out IPresentationCommand? command)
        {
            command = FindPresentationCommand(commands, commandName);
            return command != null;
        }

        /// <summary>
        /// Find <see cref="IPresentationCommand{TEventArgs}"/> with specified name.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the orinal event arguments.</typeparam>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to find.</param>
        /// <returns>A <see cref="IPresentationCommand{TEventArgs}"/> with the specified name, or <c>null</c> if not found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandName"/> is <c>null</c>, empty or only whitespace.</exception>
        public static IPresentationCommand<TEventArgs>? FindPresenationCommand<TEventArgs>(this IPresentationCommands commands, string commandName)
            where TEventArgs : EventArgs
        {
            var command = FindPresentationCommand(commands, commandName);
            return command as IPresentationCommand<TEventArgs>;
        }

        /// <summary>
        /// Get <see cref="IPresentationCommand{TEventArgs}"/> with specified name. 
        /// Throws <see cref="InvalidOperationException"/> if command with specified name does not exist.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the orinal event arguments.</typeparam>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to find.</param>
        /// <returns>A <see cref="IPresentationCommand{TEventArgs}"/> with the specified name.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandName"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If command does not exist.</exception>
        public static IPresentationCommand<TEventArgs> GetPretentationCommand<TEventArgs>(this IPresentationCommands commands, string commandName)
            where TEventArgs : EventArgs
        {
            var command = FindPresenationCommand<TEventArgs>(commands, commandName);
            return command ?? throw new InvalidOperationException($"Command '{commandName}' does not exist or is not of type '{typeof(IPresentationCommand<TEventArgs>).FullName}'.");
        }

        /// <summary>
        /// Tries to get <see cref="IPresentationCommand{TEventArgs}"/> with specified name.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the orinal event arguments.</typeparam>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandName">The name of the command to get.</param>
        /// <param name="command">The command if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the command exists; otherwise <c>false</c>.</returns>
        public static bool TryGetPresentationCommand<TEventArgs>(this IPresentationCommands commands, string commandName, [NotNullWhen(true)] out IPresentationCommand<TEventArgs>? command)
            where TEventArgs : EventArgs
        {
            command = FindPresenationCommand<TEventArgs>(commands, commandName);
            return command != null;
        }

        /// <summary>
        /// Enable all disabled commands with specified names.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandNames">The names of commands to enable.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandNames"/> is <c>null</c>.</exception>
        public static void EnableCommands(this IPresentationCommands commands, IEnumerable<string> commandNames)
        {
            ArgumentNullException.ThrowIfNull(commands);
            ArgumentNullException.ThrowIfNull(commandNames);

            foreach (var command in commands)
            {
                if (commandNames.Contains(command.Name) && !command.IsEnabled)
                    command.IsEnabled = true;
            }
        }

        /// <summary>
        /// Disable all enabled commands with specified names.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandNames">The names of commands to enable.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandNames"/> is <c>null</c>.</exception>
        public static void DisableCommands(this IPresentationCommands commands, IEnumerable<string> commandNames)
        {
            ArgumentNullException.ThrowIfNull(commands);
            ArgumentNullException.ThrowIfNull(commandNames);

            foreach (var command in commands)
            {
                if (commandNames.Contains(command.Name) && command.IsEnabled)
                    command.IsEnabled = false;
            }
        }

        /// <summary>
        /// Enable all disabled commands except those with specified names.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandNames">The names of commands to exclude from enabling.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandNames"/> is <c>null</c>.</exception>
        public static void EnableCommandsExcept(this IPresentationCommands commands, IEnumerable<string> commandNames)
        {
            ArgumentNullException.ThrowIfNull(commands);
            ArgumentNullException.ThrowIfNull(commandNames);

            foreach (var command in commands)
            {
                if (!commandNames.Contains(command.Name) && !command.IsEnabled)
                    command.IsEnabled = true;
            }
        }

        /// <summary>
        /// Disables all commands in the collection except those whose names are specified.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <param name="commandNames">The names of commands to exclude from disabling.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> or <paramref name="commandNames"/> is <c>null</c>.</exception>
        public static void DisableCommandsExcept(this IPresentationCommands commands, IEnumerable<string> commandNames)
        {
            ArgumentNullException.ThrowIfNull(commands);
            ArgumentNullException.ThrowIfNull(commandNames);

            foreach (var command in commands)
            {
                if (!commandNames.Contains(command.Name) && command.IsEnabled)
                    command.IsEnabled = false;
            }
        }

        /// <summary>
        /// Enable all disabled commands.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> is <c>null</c>.</exception>
        public static void EnableCommands(this IPresentationCommands commands)
        {
            ArgumentNullException.ThrowIfNull(commands);

            foreach (var command in commands)
            {
                if (!command.IsEnabled)
                    command.IsEnabled = true;
            }
        }

        /// <summary>
        /// Disable all enabled commands.
        /// </summary>
        /// <param name="commands">The collection of presentation commands.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="commands"/> is <c>null</c>.</exception>
        public static void DisableCommands(this IPresentationCommands commands)
        {
            ArgumentNullException.ThrowIfNull(commands);

            foreach (var command in commands)
            {
                if (command.IsEnabled)
                    command.IsEnabled = false;
            }
        }
    }
}
