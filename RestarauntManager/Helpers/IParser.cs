using RestarauntManager.DTOs;
using System.Collections.Generic;

namespace RestarauntManager.Helpers
{
    public interface IParser 
    {
        ProductDTO ParseProductForAdd(List<string> values);
        UpdateProductDTO ParseProductForUpdate(List<string> values);
        MenuItemDTO ParseMenuItemForAdd(List<string> values);
        UpdateMenuItemDTO ParseMenuItemForUpdate(List<string> values);
        OrderDTO ParseOrderForAdd(List<string> values);
        int? ParseIdForDelete(string value);
    }
}
