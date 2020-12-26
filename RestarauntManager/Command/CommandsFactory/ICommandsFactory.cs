using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Command.CommandsFactory
{
    public interface ICommandsFactory
    {
        List<ICommand> GetAllCommands();
        string GetHelperText();
    }
}
