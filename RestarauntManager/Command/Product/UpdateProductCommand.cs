using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command.Product
{
    class UpdateProductCommand : ICommand
    {
        private readonly string COMMAND_STRING = "update product";
        private readonly IProductService _productService;
        private readonly IParser _parser;

        public UpdateProductCommand(IProductService productService, IParser parser)
        {
            _productService = productService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim().Split(',').ToList();

            var product = _parser.ParseProductForUpdate(splitedString);

            if (product != null)
            {
                var result = _productService.UpdateProduct(product);

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
            return "update product [Id],[Name],[PortionCount],[Unit],[PortionSize] . Example: update product 1,Chicken,20,kg,0.3";
        }
    }
}
