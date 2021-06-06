using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.API.Controllers;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.UOW;
using ProductManagement.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProductManagement.API.Models;
using Xunit;

namespace ProductManagement.Tests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public ProductsControllerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock
                .SetupGet(uow => uow.ProductRepository)
                .Returns(_productRepositoryMock.Object);

            _sut = new ProductsController(_unitOfWorkMock.Object);
        }


        [Fact]
        public async Task GetProducts_ReturnsActionResult_WithListOfProducts()
        {
            //Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(TestData.GetTestProducts());
            
            //Act
            var actionResult = await _sut.GetProducts();
            
            //Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Product>>>(actionResult);
            Assert.Equal(2, actionResult.Value.Count());
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction_WhenModelStateIsValid()
        {
            // Arrange
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 03), DateFinValidation = new DateTime(2021, 05, 5) };
            var newProduct = new Product { Id = 1, Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 03), DateFinValidation = new DateTime(2021, 05, 5) };
            
            _productRepositoryMock
                .Setup(repo => repo.Add(It.IsAny<Product>()))
                .Returns(newProduct);
            _unitOfWorkMock
                .Setup(uow => uow.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(new Result { Status = Status.Success });

            // Act
            var actionResult = await _sut.PostProduct(newProductDto);

            // Assert
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(newProduct, createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostProduct_ReturnsBardRequest_WhenModelStateIsNotValid()
        {
            // Arrange
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };
            _sut.ModelState.AddModelError("", "error message");

            // Act
            var actionResult = await _sut.PostProduct(newProductDto);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostProduct_ReturnsBardRequest_WhenCodeIsNotUnique()
        {
            // Arrange
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };
            var newProduct = new Product { Id = 1, Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };

            _productRepositoryMock
                .Setup(repo => repo.Add(It.IsAny<Product>()))
                .Returns(newProduct);
            _unitOfWorkMock
                .Setup(uow => uow.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(new Result { Status = Status.Failure });

            // Act
            var actionResult = await _sut.PostProduct(newProductDto);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductNotExists()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Product>(null));

            // Act
            var actionResult = await _sut.GetProduct(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetProduct_ReturnsOk_WhenProductExists()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(TestData.GetTestProducts().First()));

            // Act
            var actionResult = await _sut.GetProduct(1);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        }


    }
}
