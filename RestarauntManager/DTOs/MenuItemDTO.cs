using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.DTOs
{
    public class MenuItemDTO
    {
        public string Name { get; set; }

        public List<int> Products { get; set; }
    }
}
