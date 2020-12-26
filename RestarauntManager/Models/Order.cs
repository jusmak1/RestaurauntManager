using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Models
{
    public class Order : EntityBase
    {
        public DateTime Date { get; set; }

        public List<int> MenuItems { get; set; }

        public override string ToString()
        {
            return $"{Id},{Date.ToString("yyyy-MM-dd HH:mm:ss")},{(MenuItems != null ? string.Join(" ", MenuItems) : "")}";
        }
    }
}
