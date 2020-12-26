
using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System.Linq;

namespace RestarauntManager.Command.MenuItem
{
    class DeleteMenuItemCommand : ICommand
    {
        private readonly string COMMAND_STRING = "delete menu";

        private readonly IMenuService _menuService;
        private readonly IParser _parser;
        public DeleteMenuItemCommand(IMenuService menuService, IParser parser)
        {
            _menuService = menuService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim();

            var id = _parser.ParseIdForDelete(splitedString);

            if (id == null)
            {
                return new CommandResponse { Success = false };
            }

            var result = _menuService.DeleteById(id.Value);

            return new CommandResponse { Message = result.Message, Success = result.Success };
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
            return "delete menu [Id]. Example: delete menu 1";
        }
    }
}
