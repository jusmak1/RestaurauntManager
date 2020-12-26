using AutoMapper;
using RestarauntManager.DTOs;
using RestarauntManager.Models;
using RestarauntManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
            CreateMap<MenuItemDTO, MenuItem>();
            CreateMap<UpdateMenuItemDTO, MenuItem>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
