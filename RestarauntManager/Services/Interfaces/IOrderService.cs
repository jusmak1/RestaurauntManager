using RestarauntManager.DTOs;
using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        ServiceResponse<Order> AddOrder(OrderDTO newOrder);
    }
}
