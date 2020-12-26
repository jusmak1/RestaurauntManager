using RestarauntManager.DTOs;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Services.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<MenuItem> GetAll();
        ServiceResponse<MenuItem> AddMenuItem(MenuItemDTO newMenuItem);
        ServiceResponse<MenuItem> UpdateMenuItem(UpdateMenuItemDTO newProduct);
        ServiceResponse<MenuItem> DeleteById(int id);
    }
}
