using RestarauntManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Repositories.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly IContext _context;
        public ProductRepository(IContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.GetProducts();
        }
        public Product Create(Product entity)
        {
            var products = _context.GetProducts();
            if (products == null)
                return null;

            var maxId = 0;
            
            if(products.Count() > 0)
                maxId = products.Max(product => product.Id);
           
            entity.Id = maxId + 1;

            products.Add(entity);
            if(_context.SetProducts(products))
            {
                return entity;
            }
            return null;
        }

        public void Delete(int id)
        {
            var newProducts = _context.GetProducts().Where(product => product.Id != id).ToList();

            _context.SetProducts(newProducts);
        }


        public Product GetById(int id)
        {
            var products = _context.GetProducts();
            return products.FirstOrDefault(product => id == product.Id);
        }

        public bool Update(Product entity)
        {
            var product = GetById(entity.Id);

            if (product == null) return false;

            product.Name = entity.Name;
            product.PortionCount = entity.PortionCount;
            product.PortionSize = entity.PortionSize;
            product.Unit = entity.Unit;

            var allProducts = GetAll().Where(product => product.Id != entity.Id).ToList();

            allProducts.Add(entity);

            allProducts = allProducts.OrderBy(product => product.Id).ToList();

            if (_context.SetProducts(allProducts))
            {
                return true;
            }

            return false;
        }
    }

}
