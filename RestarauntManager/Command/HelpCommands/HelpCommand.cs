using RestarauntManager.Command.CommandsFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Command.HelpCommands
{
    public class HelpCommand : ICommand
    {
        private readonly string COMMAND_STRING = "/help";

        private readonly ICommandsFactory _commandFactory;

        public HelpCommand(ICommandsFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public CommandResponse Execute(string commandString)
        {
            return new CommandResponse { Message = _commandFactory.GetHelperText() };
        }

        public string GetHelperText()
        {
            return "/help";
        }

        public bool Matches(string commandString)
        {
            if (!string.IsNullOrEmpty(commandString) && commandString.ToLower().StartsWith(COMMAND_STRING))
            {
                return true;
            }
            return false;
        }
    }
}
