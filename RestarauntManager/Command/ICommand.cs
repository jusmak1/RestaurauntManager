using RestarauntManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Command
{
    public interface ICommand
    {
        bool Matches(string commandString);
        CommandResponse Execute(string commandString);

        string GetHelperText();
    }
}
