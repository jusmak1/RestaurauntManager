using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.DTOs
{
    public class OrderDTO
    {
        public DateTime? Date { get; set; }
        public List<int> MenuItems { get; set; }
    }
}
