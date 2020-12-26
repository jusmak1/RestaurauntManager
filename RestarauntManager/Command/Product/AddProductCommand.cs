using RestarauntManager.Helpers;
using RestarauntManager.Models;
using RestarauntManager.Services;
using RestarauntManager.Services.Interfaces;
using System.Linq;

namespace RestarauntManager.Command
{
    class AddProductCommand : ICommand
    {

        private readonly string COMMAND_STRING = "add product";
        private readonly IProductService _productService;
        private readonly IParser _parser;

        public AddProductCommand(IProductService productService, IParser parser)
        {
            _productService = productService;
            _parser = parser;
        }
        public CommandResponse Execute(string commandString)
        {
            var splitedString = commandString.Replace(COMMAND_STRING, "").Trim().Split(',').ToList();

            var product = _parser.ParseProductForAdd(splitedString);

            if(product != null)
            {
                var result = _productService.AddProduct(product);

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
            return "add product [Name],[PortionCount],[Unit],[PortionSize] . Example: add product Chicken,10,kg,0.3";
        }

    }
 }

