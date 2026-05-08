using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Windows.Presentation
{
    public static class PresentationCommandsHelper
    {
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

        public static IPresentationCommand GetPresentationCommand(this IPresentationCommands commands, string commandName)
        {
            var command = FindPresentationCommand(commands, commandName);
            return command ?? throw new InvalidOperationException($"Command '{commandName}' does not exist.");
        }

        public static bool TryGetPresentationCommand(this IPresentationCommands commands, string commandName, [NotNullWhen(true)] out IPresentationCommand? command)
        {
            command = FindPresentationCommand(commands, commandName);
            return command != null;
        }

        public static IPresentationCommand<TEventArgs>? FindPresenationCommand<TEventArgs>(this IPresentationCommands commands, string commandName)
            where TEventArgs : EventArgs
        {
            var command = FindPresentationCommand(commands, commandName);
            return command as IPresentationCommand<TEventArgs>;
        }

        public static IPresentationCommand<TEventArgs> GetPretentationCommand<TEventArgs>(this IPresentationCommands commands, string commandName)
            where TEventArgs : EventArgs
        {
            var command = FindPresenationCommand<TEventArgs>(commands, commandName);
            return command ?? throw new InvalidOperationException($"Command '{commandName}' does not exist or is not of type '{typeof(IPresentationCommand<TEventArgs>).FullName}'.");
        }

        public static bool TryGetPresentationCommand<TEventArgs>(this IPresentationCommands commands, string commandName, [NotNullWhen(true)] out IPresentationCommand<TEventArgs>? command)
            where TEventArgs : EventArgs
        {
            command = FindPresenationCommand<TEventArgs>(commands, commandName);
            return command != null;
        }

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
    }
}
