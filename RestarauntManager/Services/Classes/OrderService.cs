using AutoMapper;
using RestarauntManager.DTOs;
using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestarauntManager.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public OrderService(IOrderRepository orderRepository, IMenuRepository menuRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _menuRepository = menuRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

       

        public ServiceResponse<Order> AddOrder(OrderDTO newOrder)
        {
            try
            {
                var allMenuItems = _menuRepository.GetAll();
                var allProducts = _productRepository.GetAll();

                if (allMenuItems == null || allProducts == null)
                {
                    return new ServiceResponse<Order> { Success = false, Message = $"Cannot add because {(allMenuItems == null ? "menu items" : "products")} file is corrupted)" };
                }

                var neededStockForEveryOrder = GetNeededStockForEveryOrder(newOrder.MenuItems, allMenuItems);

                CheckIfEnoughStock(allProducts, neededStockForEveryOrder);

                UpdateProductsStocks(allProducts, neededStockForEveryOrder);

                if(newOrder.Date == null)
                {
                    newOrder.Date = DateTime.Now;
                }

                var result = _orderRepository.Create(_mapper.Map<Order>(newOrder));

                if(result != null)
                {
                   return new ServiceResponse<Order> { Message = $"Successfully order with Id: {result.Id}", Data = result };
                }
   
                return new ServiceResponse<Order> { Success = false };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Order> { Success = false, Message = $"Error adding order. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}" };
            }
        }

        private Dictionary<int, int> GetNeededStockForEveryOrder(List<int> menuItemsIds, List<MenuItem> allMenuItems)
        {
            var result = new Dictionary<int, int>();
            foreach(var menuItemId in menuItemsIds)
            {
                var product = allMenuItems.Where(menuItem => menuItem.Id == menuItemId).FirstOrDefault();
                if (product == null)
                {
                    throw new ArgumentException($"Menu Item with id: {menuItemId} does not exist");
                }

                foreach(var productId in product.Products)
                {
                    if (result.ContainsKey(productId))
                    {
                        result[productId]++;
                    }
                    else
                    {
                        result[productId] = 1;
                    }
                }
            }

            return result;
        }

        private void UpdateProductsStocks(List<Product> allProducts, Dictionary<int, int> neededStockForEveryProduct)
        {
            foreach(var neededProductId in neededStockForEveryProduct.Keys)
            {
                var product = allProducts.First(product => neededProductId == product.Id);

                product.PortionCount -= neededStockForEveryProduct[neededProductId];

                _productRepository.Update(product);
            }
        }

        private void CheckIfEnoughStock(List<Product> allProducts, Dictionary<int, int> neededStockForEveryProduct)
        {
            foreach(var productId in neededStockForEveryProduct.Keys)
            {
                var product = allProducts.FirstOrDefault(product => product.Id == productId);
                if(product == null)
                {
                    throw new Exception($"Cant find product with id: {productId}. Make sure it exists");
                }

                if (product.PortionCount - neededStockForEveryProduct[productId] < 0)
                {
                    throw new Exception($"Order canceled. Not enough stock for product with id: {productId}. Needs {-1 * (product.PortionCount - neededStockForEveryProduct[productId])} more");
                }
            }
        }
    }
}
