using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Windows.Presentation
{
    public sealed class PresentationCommandCollection : IPresentationCommands
    {
        private readonly Dictionary<string, IPresentationCommand> commands;

        public PresentationCommandCollection()
        {
            commands = new Dictionary<string, IPresentationCommand>();
        }

        public IPresentationCommand CreateCommand(string commandName)
        {
            CheckIsAvailableCommandName(commandName);
            var command = new PresentationCommand(commandName);
            commands.Add(commandName, command);
            return command;
        }

        public IPresentationCommand<TEventArgs> CreateCommand<TEventArgs>(string commandName)
            where TEventArgs : EventArgs
        {
            CheckIsAvailableCommandName(commandName);
            var command = new PresentationCommand<TEventArgs>(commandName);
            commands.Add(commandName, command);
            return command;
        }

        public IEnumerator<IPresentationCommand> GetEnumerator()
        {
            var commandList = commands.Values.ToList();

            foreach (var command in commandList)
                yield return command;
        }

        private void CheckIsAvailableCommandName(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentException("Command name cannot be null or whitespace.", nameof(commandName));
            
            if (commands.ContainsKey(commandName))
                throw new ArgumentException("A command with the same name already exists.", nameof(commandName));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
