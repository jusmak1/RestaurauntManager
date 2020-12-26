using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.DTOs
{
    public class UpdateMenuItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Products { get; set; }
    }
}
