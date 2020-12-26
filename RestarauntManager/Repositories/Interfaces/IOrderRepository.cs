using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Repositories.Interfaces
{
    /// <summary>
    /// Implements IRepository. Can add additional functionality for IOrderRepository if needed here.
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {

    }
}
