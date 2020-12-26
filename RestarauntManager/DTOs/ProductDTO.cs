using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }

        public int PortionCount { get; set; }

        public string Unit { get; set; }

        public double PortionSize { get; set; }
    }
}
