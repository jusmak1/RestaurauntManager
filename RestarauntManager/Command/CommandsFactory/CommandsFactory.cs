using RestarauntManager.Command.HelpCommands;
using RestarauntManager.Command.MenuItem;
using RestarauntManager.Command.Order;
using RestarauntManager.Command.Product;
using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Command.CommandsFactory
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IParser _parser;
        private readonly IProductService _productService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;

        public CommandsFactory(IProductService productService, IMenuService menuService, IOrderService orderService, IParser parser)
        {
            _productService = productService;
            _menuService = menuService;
            _orderService = orderService;
            _parser = parser;
        }
        /// <summary>
        /// List of all available user commands
        /// </summary>
        /// <returns></returns>
        public List<ICommand> GetAllCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            commands.Add(new AddProductCommand(_productService, _parser));
            commands.Add(new DisplayAllProductsCommand(_productService));
            commands.Add(new UpdateProductCommand(_productService, _parser));
            commands.Add(new DeleteProductCommand(_productService, _parser));
            commands.Add(new DisplayAllMenuItemsCommand(_menuService));
            commands.Add(new AddNewMenuItemCommand(_menuService, _parser));
            commands.Add(new UpdateMenuItemCommand(_menuService, _parser));
            commands.Add(new DeleteMenuItemCommand(_menuService, _parser));
            commands.Add(new DisplayAllOrdersCommand(_orderService));
            commands.Add(new AddOrderCommand(_orderService, _parser));
            commands.Add(new HelpCommand(this));

            return commands;
        }

        public string GetHelperText()
        {
            var commands = GetAllCommands();
            var sb = new StringBuilder();

            sb.AppendLine("Available commands");
            sb.AppendLine(new string('-', 25));
            foreach(var command in commands)
            {
                sb.AppendLine(command.GetHelperText());
            }
            sb.AppendLine(new string('-', 25));

            return sb.ToString();
        }
    }
}
