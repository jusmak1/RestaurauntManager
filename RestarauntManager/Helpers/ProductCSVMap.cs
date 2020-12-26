using CsvHelper.Configuration;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Helpers
{
    public class ProductCSVMap : ClassMap<Product>
    {
        public ProductCSVMap()
        {
            Map(x => x.Id).Index(0);
            Map(x => x.Name).Index(1);
            Map(x => x.PortionCount).Index(2);
            Map(x => x.Unit).Index(3);
            Map(x => x.PortionSize).Index(4);
        }
    }
}
