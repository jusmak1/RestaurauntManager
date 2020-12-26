using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Models
{
    public class MenuItem : EntityBase
    {
        public string Name { get; set; }

        public List<int> Products { get; set; }

        public override string ToString()
        {
            return $"{Id},{Name},{(Products != null ? string.Join(" ", Products) : "")}";
        }
    }
}
