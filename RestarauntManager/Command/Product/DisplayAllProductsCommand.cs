using RestarauntManager.Services.Interfaces;
using System.Linq;
using System.Text;

namespace RestarauntManager.Command
{
    class DisplayAllProductsCommand : ICommand
    {
        private readonly string COMMAND_STRING = "get products";

        public readonly IProductService _productService;

        public DisplayAllProductsCommand(IProductService productService)
        {
            _productService = productService;
        }
        public CommandResponse Execute(string commandString)
        {

            var result = _productService.GetAll();

            if (result == null)
                return new CommandResponse { Success = false };

            var sb = new StringBuilder();

            if(result.Count() > 0)
            {
                foreach (var product in result)
                {
                    sb.AppendLine(product.ToString());
                }

            }
            else
            {
                sb.AppendLine("No products to display");
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
            return "get products";
        }
    }
}
