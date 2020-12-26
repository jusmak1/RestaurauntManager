using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System.Linq;

namespace RestarauntManager.Command.MenuItem
{
    public class AddNewMenuItemCommand : ICommand
    {
        private readonly string COMMAND_STRING = "add menu";
        private readonly IMenuService _menuService;
        private readonly IParser _parser;

        public AddNewMenuItemCommand(IMenuService menuService, IParser parser)
        {
            _menuService = menuService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim().Split(',').ToList();

            var product = _parser.ParseMenuItemForAdd(splitedString);

            if (product != null)
            {
                var result = _menuService.AddMenuItem(product);

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
            return "add menu [Name],[ProductId1],[ProductId2] ... Example: add menu Kebab,1 2";
        }
    }
}
