using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Repositories.Classes
{
    public class MenuRepository : IMenuRepository
    {

        private readonly IContext _context;
        public MenuRepository(IContext context)
        {
            _context = context;
        }

        public List<MenuItem> GetAll()
        {
            return _context.GetMenuItems();
        }

        public MenuItem GetById(int id)
        {
            var all = GetAll();
            return all.FirstOrDefault(menuItem => id == menuItem.Id);
        }
        public MenuItem Create(MenuItem entity)
        {
            var allMenuItems = GetAll();

            if (allMenuItems == null)
                return null;

            var maxId = 0;

            if(allMenuItems.Count > 0)
                maxId = allMenuItems.Max(menuItem => menuItem.Id);

            entity.Id = maxId + 1;
            allMenuItems.Add(entity);

            if (_context.SetMenuItems(allMenuItems))
            {
                return entity;
            }
            return null;
        }

        public bool Update(MenuItem entity)
        {
            var allMenuItems = GetAll();

            if (allMenuItems == null)
                return false;

            var oldMenuEntity = allMenuItems.Where(menuItem => menuItem.Id == entity.Id).FirstOrDefault();

            if (oldMenuEntity == null)
                return false;

            oldMenuEntity.Name = entity.Name;
            oldMenuEntity.Products = entity.Products;

            allMenuItems = allMenuItems.Where(menuItem => menuItem.Id != entity.Id).ToList();
            allMenuItems.Add(entity);
            allMenuItems = allMenuItems.OrderBy(menuItem => menuItem.Id).ToList();

            if (_context.SetMenuItems(allMenuItems))
            {
                return true;
            }

            return false;
        }

        public void Delete(int id)
        {
            var allMenuItems = GetAll();

            allMenuItems = allMenuItems.Where(menuItem => menuItem.Id != id).ToList();

            _context.SetMenuItems(allMenuItems);
        }

      
    }
}
