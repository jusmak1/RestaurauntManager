using CsvHelper.Configuration;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Helpers
{
    public class MenuItemCSVMap : ClassMap<MenuItem>
    {
        public MenuItemCSVMap()
        {
            Map(x => x.Id).Index(0);
            Map(x => x.Name).Index(1);
            Map(x => x.Products).Index(2).ConvertUsing(row =>
            {
                try
                {
                    return row.GetField("Products").Split(" ").Select(productId => int.Parse(productId)).ToList();
                }catch(Exception e) { return null; }
            });
        }
    }
}
