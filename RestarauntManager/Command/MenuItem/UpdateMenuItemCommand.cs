using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command.MenuItem
{
    class UpdateMenuItemCommand : ICommand
    {
        private readonly string COMMAND_STRING = "update menu";
        private readonly IMenuService _menuService;
        private readonly IParser _parser;

        public UpdateMenuItemCommand(IMenuService menuService, IParser parser)
        {
            _menuService = menuService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim().Split(',').ToList();

            var product = _parser.ParseMenuItemForUpdate(splitedString);

            if (product != null)
            {
                var result = _menuService.UpdateMenuItem(product);

                return new CommandResponse { Message = result.Message, Success = result.Success };
            }

            return new CommandResponse { Success = false };
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
            return "update menu [Id],[Name],[ProductId1],[ProductId2] ... Eaxmple: update menu KebabBetter,1 2 3";
        }
    }
}
