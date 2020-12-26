using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Models
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public int PortionCount { get; set; }

        public string Unit { get; set; }

        public double PortionSize { get; set; }

        public override string ToString()
        {
            return $"{Id},{Name},{PortionCount},{Unit},{PortionSize}";
        }
    }
}
