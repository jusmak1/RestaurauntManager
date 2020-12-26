using AutoMapper;
using Moq;
using RestarauntManager.Helpers;
using RestarauntManager.Models;
using RestarauntManager.Repositories.Interfaces;
using RestarauntManager.Services.Classes;
using RestarauntManager.Services.Interfaces;
using RestarauntManager.DTOs;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using RestarauntManager.Services;
using System;

namespace RestarauntManagarer.Tests.Services
{
    public class ProductsServiceTests
    {

        private MockRepository mockRepository;
        private Mock<IProductRepository> _productRepositoryMock;
        private IProductService _productService;

        public ProductsServiceTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);

            _productRepositoryMock = mockRepository.Create<IProductRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); 
            });
            var mapper = mockMapper.CreateMapper();

            _productService = new ProductService(_productRepositoryMock.Object, mapper);
        }

        private static List<Product> GetProductsList()
        {
            var result = new List<Product>();

            result.Add(new Product
            {
                Id = 1,
                Name = "Chicken",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            result.Add(new Product
            {
                Id = 1,
                Name = "Potatoes",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            return result;
        }

        [Fact]
        public void GetAllReturnsAllProducts()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetAll()).Returns(GetProductsList());

            //Act
            var result = _productService.GetAll();

            //Assert
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public void GetAllReturnsEmptyListIfNoProducts()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetAll()).Returns(new List<Product>());

            //Act
            var result = _productService.GetAll();

            //Assert
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);

        }

        [Fact]
        public void ReturnSuccessIfAdded()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.Create(It.IsAny<Product>())).Returns(new Product
            {
                Id = 1,
                Name = "Chicken",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });


            //Act
            var result = _productService.AddProduct(new ProductDTO 
            {
                Name = "Chicken",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.Equal(1, result.Data.Id);
            Assert.True(result.Success);
            Assert.Equal($"Succesfully added product: Id: {1}", result.Message);
        }

        [Fact]
        public void ReturnFailIfNotAdded()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.Create(It.IsAny<Product>())).Throws(new Exception());


            //Act
            var result = _productService.AddProduct(new ProductDTO
            {
                Name = "Chicken",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.False(result.Success);
            Assert.NotEqual("" ,result.Message);
        }

        [Fact]
        public void UpdateReturnsFailIfProductDoesNotExist()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(() => null);

            //Act
            var result = _productService.UpdateProduct(new UpdateProductDTO
            {
                Id = 1,
                Name = "Chicken",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.False(result.Success);
            Assert.Equal($"Product with Id: 1 does not exist", result.Message);
        }

        [Fact]
        public void UpdateReturnsSuccessIfUpdated()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Product { Id = 1 });
            _productRepositoryMock.Setup(repository => repository.Update(It.IsAny<Product>())).Returns(true);


            //Act
            var result = _productService.UpdateProduct(new UpdateProductDTO
            {
                Id = 1,
                Name = "updated",
                PortionCount = 10,
                Unit = "kg",
                PortionSize = 0.3
            });

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.True(result.Success);
            Assert.Equal($"Succesfully updated product: Id: 1", result.Message);
        }

        [Fact]
        public void DeleteReturnsFailIfProductDoesNotExist()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(() => null);

            //Act
            var result = _productService.DeleteById(1);

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.False(result.Success);
            Assert.Equal($"Product with Id: 1 does not exist", result.Message);
        }

        [Fact]
        public void DeleteReturnsSuccessIfProductDeletedt()
        {
            //Arange
            _productRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(() => new Product {  Id = 1 });
            _productRepositoryMock.Setup(repository => repository.Delete(It.IsAny<int>())).Verifiable();

            //Act
            var result = _productService.DeleteById(1);

            //Assert
            Assert.IsType<ServiceResponse<Product>>(result);
            Assert.True(result.Success);
            Assert.Equal($"Succesfully deleted product: Id: 1", result.Message);
        }

    }
}
