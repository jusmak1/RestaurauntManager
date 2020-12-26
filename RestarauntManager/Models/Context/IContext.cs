using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Models
{
    public interface IContext
    {
        List<Product> GetProducts();
        bool SetProducts(IEnumerable<Product> products);
        List<MenuItem> GetMenuItems();
        bool SetMenuItems(IEnumerable<MenuItem> menuItems);
        List<Order> GetOrders();
        bool SetOrders(List<Order> orders);       
    }
}
