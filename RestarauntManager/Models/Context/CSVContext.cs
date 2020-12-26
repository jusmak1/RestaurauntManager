using CsvHelper;
using Microsoft.Extensions.Logging;
using RestarauntManager.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RestarauntManager.Models
{
    class CSVContext : IContext
    {

        private const string PRODUCTS_FILE = "products.csv";
        private const string MENU_ITEMS_FILE = "menu.csv";
        private const string ORDERS_FILE = "orders.csv";

        private readonly ILogger _logger;

        public CSVContext(ILogger<CSVContext> logger)
        {
            _logger = logger;

        }

        public List<MenuItem> GetMenuItems()
        {
            try
            {
                var fileDirecotry = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var reader = new StreamReader($"{fileDirecotry}\\{MENU_ITEMS_FILE}"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<MenuItemCSVMap>();
                        return csv.GetRecords<MenuItem>().ToList();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error trying access menu items from file: {PRODUCTS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                var fileDirecotry = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var reader = new StreamReader($"{fileDirecotry}\\{ORDERS_FILE}"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<OrderCSVMap>();
                        return csv.GetRecords<Order>().ToList();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error trying access orders from file: {ORDERS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
        }

        public List<Product> GetProducts()
        {
            try
            {
                var fileDirecotry =Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var reader = new StreamReader($"{fileDirecotry}\\{PRODUCTS_FILE}"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        return csv.GetRecords<Product>().ToList();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error trying access prdocuts from file: {PRODUCTS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return null;
            }
        }

        public bool SetMenuItems(IEnumerable<MenuItem> menuItems)
        {
            try
            {
                var fileDirecotry = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var writer = new StreamWriter($"{fileDirecotry}\\{MENU_ITEMS_FILE}", false))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<MenuItemCSVMap>();
                        csv.WriteHeader<MenuItem>();
                        foreach(var menuItem in menuItems)
                        {
                            csv.NextRecord();
                            csv.WriteConvertedField(menuItem.Id.ToString());
                            csv.WriteConvertedField(menuItem.Name);
                            csv.WriteConvertedField(string.Join(" ", menuItem?.Products));
                        }
                        
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding menuItem to file: {PRODUCTS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return false;
            }
        }

        public bool SetOrders(List<Order> orders)
        {
            try
            {
                var fileDirecotry = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var writer = new StreamWriter($"{fileDirecotry}\\{ORDERS_FILE}", false))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<OrderCSVMap>();
                        csv.WriteHeader<Order>();
                        foreach (var order in orders)
                        {
                            csv.NextRecord();
                            csv.WriteConvertedField(order.Id.ToString());
                            csv.WriteConvertedField(order.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                            csv.WriteConvertedField(string.Join(" ", order?.MenuItems));
                        }
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding order to file: {ORDERS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return false;
            }
        }

        public bool SetProducts(IEnumerable<Product> products)
        {
            try
            {
                var fileDirecotry = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
                using (var writer = new StreamWriter($"{fileDirecotry}\\{PRODUCTS_FILE}", false))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.RegisterClassMap<ProductCSVMap>();
                        csv.WriteRecords(products);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding product to file: {PRODUCTS_FILE}. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}");
                return false;
            }
        }
    }
}
