using RestarauntManager.DTOs;
using RestarauntManager.Models;
using System.Collections.Generic;

namespace RestarauntManager.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        ServiceResponse<Product> AddProduct(ProductDTO newProduct);
        ServiceResponse<Product> UpdateProduct(UpdateProductDTO newProduct);
        ServiceResponse<Product> DeleteById(int id);
    }
}
