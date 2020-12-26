using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command.MenuItem
{
    public class DisplayAllMenuItemsCommand : ICommand
    {

        private readonly string COMMAND_STRING = "get menu";

        public readonly IMenuService _menuService;

        public DisplayAllMenuItemsCommand(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public CommandResponse Execute(string commandString)
        {

            var result = _menuService.GetAll();

            if (result == null)
                return new CommandResponse { Success = false };

            var sb = new StringBuilder();
            if (result.Count() > 0)
            {
                foreach (var menu in result)
                {
                    sb.AppendLine(menu.ToString());
                }
            }
            else
            {
                sb.AppendLine("No menu items to display");
            }
            

            return new CommandResponse { Message = sb.ToString() };
        }

        public bool Matches(string commandString)
        {
            if (!string.IsNullOrEmpty(commandString) && commandString.ToLower().StartsWith(COMMAND_STRING))
            {
                return true;
            }
            return false;
        }

        public string GetHelperText()
        {
            return "get menu"; 
        }
    }
}
