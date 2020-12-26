using AutoMapper;
using Microsoft.Extensions.Logging;
using RestarauntManager.DTOs;
using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using RestarauntManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestarauntManager.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public ServiceResponse<Product> AddProduct(ProductDTO newProduct)
        {
            try
            {
                var product = _productRepository.Create(_mapper.Map<Product>(newProduct));

                return new ServiceResponse<Product> { Data = product, Message = $"Succesfully added product: Id: { product.Id }" };

            }
            catch (Exception e)
            {
                var errorMesage = $"Error adding product: Error message: {e.Message + " " + e.InnerException?.Message ?? ""}";
                return new ServiceResponse<Product> { Success = false, Message = errorMesage };
            }
        }

        public ServiceResponse<Product> UpdateProduct(UpdateProductDTO newProduct)
        {
            try
            {
                var product = _productRepository.GetById(newProduct.Id);

                if(product == null)
                {
                    return new ServiceResponse<Product> { Success = false, Message = $"Product with Id: {newProduct.Id} does not exist" };
                }

                 _productRepository.Update(_mapper.Map<Product>(newProduct));

                return new ServiceResponse<Product> {  Message = $"Succesfully updated product: Id: { product.Id }"
            };
            }catch(Exception e)
            {
                var errorMesage = $"Error updating product: Error message: {e.Message + " " + e.InnerException?.Message ?? ""}";
                return new ServiceResponse<Product> { Success = false, Message = errorMesage };
            }

        }

        public ServiceResponse<Product> DeleteById(int id)
        {
            try
            {
                var product = _productRepository.GetById(id);

                if (product == null)
                {
                    return new ServiceResponse<Product> { Success = false, Message = $"Product with Id: {id} does not exist" };
                }

                _productRepository.Delete(id);

                return new ServiceResponse<Product>
                {
                    Message = $"Succesfully deleted product: Id: { product.Id }"
                };
            }
            catch (Exception e)
            {
                var errorMesage = $"Error deleting product: Error message: {e.Message + " " + e.InnerException?.Message ?? ""}";
                return new ServiceResponse<Product> { Success = false, Message = errorMesage };
            }
        }
    }

}