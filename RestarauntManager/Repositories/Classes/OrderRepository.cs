using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RestarauntManager.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;

        public OrderRepository(IContext context)
        {
            _context = context;
        }
        public List<Order> GetAll()
        {
            return _context.GetOrders();           
        }

        public Order GetById(int id)
        {
            throw new System.NotImplementedException();
        }
        public Order Create(Order entity)
        {
            var orders = GetAll();
            if (orders == null)
                return null;

            var maxId = 0;
            if(orders.Count() > 0) 
                maxId = orders.Max(order => order.Id);

            entity.Id = maxId + 1;
            orders.Add(entity);
            if (_context.SetOrders(orders))
            {
                return entity;
            }
            return null;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Order entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
