using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command.Order
{
    public class AddOrderCommand : ICommand
    {
        private readonly string COMMAND_STRING = "add order";
        private readonly IOrderService _orderService;
        private readonly IParser _parser;

        public AddOrderCommand(IOrderService orderService, IParser parser)
        {
            _orderService = orderService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim().Split(',').ToList();

            var product = _parser.ParseOrderForAdd(splitedString);

            if (product != null)
            {
                var result = _orderService.AddOrder(product);

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
            return "add order [MenuItemId1] [MenuItemId2]... .Example add order 1 2";
        }
    }
}
