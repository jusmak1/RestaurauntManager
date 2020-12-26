using CsvHelper.Configuration;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Helpers
{
    public class OrderCSVMap : ClassMap<Order>
    {
        public OrderCSVMap()
        {
            Map(x => x.Id).Index(0);
            Map(x => x.Date).Index(1);
            Map(x => x.MenuItems).Index(2).ConvertUsing(row =>
            {
                try
                {
                    return row.GetField("MenuItems").Split(" ").Select(productId => int.Parse(productId)).ToList();
                }
                catch (Exception e) { return null; }
            });
        }
    }
}
