using AutoMapper;
using RestarauntManager.DTOs;
using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestarauntManager.Services.Classes
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository menuRepository, IProductRepository productRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<MenuItem> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public ServiceResponse<MenuItem> AddMenuItem(MenuItemDTO newMenuItem)
        {
            try
            {
                if(newMenuItem.Products.Count != newMenuItem.Products.Distinct().Count())
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"There can only be one of each product" };
                }

                var allProducts = _productRepository.GetAll();

                if (allProducts == null)
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"Cannot add because products file is corrupted" };
                }

                foreach (var productId in newMenuItem.Products)
                {
                    var found = allProducts.FirstOrDefault(product => product.Id == productId);
                    if (found == null)
                    {
                        return new ServiceResponse<MenuItem> { Success = false, Message = $"Product with id: {productId} does not exist" };
                    }
                }


                var result = _menuRepository.Create(_mapper.Map<MenuItem>(newMenuItem));

                if (result != null)
                    return new ServiceResponse<MenuItem> { Message = $"Successfully added new menu Item with Id: {result.Id}", Data = result };
                return new ServiceResponse<MenuItem> { Success = false };
            }
            catch(Exception e)
            {
                return new ServiceResponse<MenuItem> { Success = false, Message = $"Error adding menu item. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}" };
            }
           
        }
  
        public ServiceResponse<MenuItem> UpdateMenuItem(UpdateMenuItemDTO newMenuItem)
        {
            try
            {
                if (newMenuItem.Products.Count != newMenuItem.Products.Distinct().Count())
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"There can only be one of each product" };
                }

                var oldMenuItme = _menuRepository.GetById(newMenuItem.Id);

                if (oldMenuItme == null)
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"Menu item with id: {newMenuItem.Id} doest not exist" };
                }

                var allProducts = _productRepository.GetAll();

                if (allProducts == null)
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"Cannot update because products file is corrupted" };
                }

                foreach (var productId in newMenuItem.Products)
                {
                    var found = allProducts.FirstOrDefault(product => product.Id == productId);
                    if (found == null)
                    {
                        return new ServiceResponse<MenuItem> { Success = false, Message = $"Product with id: {productId} does not exist" };
                    }
                }


                var success = _menuRepository.Update(_mapper.Map<MenuItem>(newMenuItem));
                if (success)
                    return new ServiceResponse<MenuItem> { Message = $"Successfully updated Item with Id: {newMenuItem.Id}" };
                return new ServiceResponse<MenuItem> { Success = false };
            }
            catch (Exception e)
            {
                return new ServiceResponse<MenuItem> { Success = false, Message = $"Error updating menu item. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}" };
            }

        }

        public ServiceResponse<MenuItem> DeleteById(int id)
        {
            try
            {
                var oldMenuItme = _menuRepository.GetById(id);

                if (oldMenuItme == null)
                {
                    return new ServiceResponse<MenuItem> { Success = false, Message = $"Menu item with id: {id} doest not exist" };
                }

   
                _menuRepository.Delete(id);
                return new ServiceResponse<MenuItem> { Message = $"Successfully deleted menu item with Id: {id}" };
            }
            catch(Exception e)
            {
                return new ServiceResponse<MenuItem> { Success = false, Message = $"Error deleting  item. Error message: {e.Message + " " + e.InnerException?.Message ?? ""}" };
            }

        }
    }
}
