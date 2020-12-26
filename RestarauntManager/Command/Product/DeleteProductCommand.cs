using RestarauntManager.Helpers;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command.Product
{
    class DeleteProductCommand : ICommand
    {

        private readonly string COMMAND_STRING = "delete product";

        private readonly IProductService _productService;
        private readonly IParser _parser;
        public DeleteProductCommand(IProductService productService, IParser parser)
        {
            _productService = productService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim();

            var id = _parser.ParseIdForDelete(splitedString);

            if(id == null)
            {
                return new CommandResponse { Success = false };
            }

            var result = _productService.DeleteById(id.Value);

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
            return "delete product [Id] .Example: delete product 1";
        }
    }
}
