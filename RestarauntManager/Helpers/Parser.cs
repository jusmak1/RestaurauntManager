using Microsoft.Extensions.Logging;
using RestarauntManager.DTOs;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Helpers
{
    public  class Parser : IParser
    {
        private readonly ILogger<Parser> _logger;
        public Parser(ILogger<Parser> logger)
        {
            _logger = logger;
        }

        public ProductDTO ParseProductForAdd(List<string> values)
        {
            if(values.Count() < 4)
            {
                _logger.LogError(@"Error parsing while adding product. Not enough arguments");
                return null;
            }
            try
            {
                var product = new ProductDTO
                {
                    Name = values[0],
                    PortionCount = int.Parse(values[1]),
                    Unit = values[2],
                    PortionSize = double.Parse(values[3])
                };

                if(product.PortionCount < 0)
                {
                    _logger.LogError(@"Portion Count can't be negative integer");
                    return null;
                }

                if(product.PortionSize < 0)
                {
                    _logger.LogError(@"Portion size can't be negative integer");
                    return null;
                }

                return product;

            }
            catch(Exception e)
            {
                _logger.LogError($"Error parsing while adding product. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
           

        }

        public UpdateProductDTO ParseProductForUpdate(List<string> values)
        {
            if (values.Count() < 5)
            {
                _logger.LogError(@"Error parsing while updating product. Not enough arguments");
                return null;
            }
            try
            {
                var product = new UpdateProductDTO
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    PortionCount = int.Parse(values[2]),
                    Unit = values[3],
                    PortionSize = double.Parse(values[4])
                };

                if (product.PortionCount < 0)
                {
                    _logger.LogError(@"Portion Count can't be negative integer");
                    return null;
                }

                if (product.PortionSize < 0)
                {
                    _logger.LogError(@"Portion size can't be negative integer");
                    return null;
                }

                return product;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error parsing while updating product. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
        }

        public int? ParseIdForDelete(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _logger.LogError(@"Error parsing while updating product. Not enough arguments");
                return null;
            }
            try
            {
                return int.Parse(value);
            }catch(Exception e)
            {
                _logger.LogError($"Error while parsing id. Error message: {e.Message + " " + e.InnerException?.Message }");
                return null;
            }
        }

        public MenuItemDTO ParseMenuItemForAdd(List<string> values)
        {
            if(values.Count < 2)
            {
                _logger.LogError(@"Error parsing while adding product. Not enough arguments");
                return null;
            }

            try
            {
                return new MenuItemDTO
                {
                    Name = values[0],
                    Products = values[1].Split(" ").Select(x => int.Parse(x)).ToList()
                };

            }catch(Exception e)
            {
                _logger.LogError($"Error parsing while adding menu item. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }

        }

        public UpdateMenuItemDTO ParseMenuItemForUpdate(List<string> values)
        {
            if (values.Count < 3)
            {
                _logger.LogError(@"Error parsing while adding product. Not enough arguments");
                return null;
            }

            try
            {
                return new UpdateMenuItemDTO
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    Products = values[2].Split(" ").Select(x => int.Parse(x)).ToList()
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error parsing while updating menu item. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }

        }

        public OrderDTO ParseOrderForAdd(List<string> values)
        {
            if (values.Count < 1 || string.IsNullOrEmpty(values[0]))
            {
                _logger.LogError(@"Error parsing while adding order. Not enough arguments");
                return null;
            }

            try
            {
                return new OrderDTO
                {
                    MenuItems = values[0].Split(" ").Select(x => int.Parse(x)).ToList()
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Error parsing while adding order. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
        }
    }
}
