using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.API.Controllers;
using ProductManagement.API.Dto;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.UOW;
using ProductManagement.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagement.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetProducts_ReturnsActionResult_WithListOfProducts()
        {
            
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(TestData.GetTestProducts());
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(uow => uow.ProductRepository).Returns(mockRepo.Object);

            var controller = new ProductsController(mockUow.Object);

            var actionResult = await controller.GetProducts();
            
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Product>>>(actionResult);
            Assert.Equal(2, actionResult.Value.Count());
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction_WhenModelStateIsValid()
        {
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 03), DateFinValidation = new DateTime(2021, 05, 5) };
            var newProduct = new Product { Id = 1, Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 03), DateFinValidation = new DateTime(2021, 05, 5) };

            var controller = new ProductsController(SettingUpUowForPostProduct(newProduct,Status.Success));
            

            var actionResult = await controller.PostProduct(newProductDto);
            var createdAtActionResult = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(newProduct, createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostProduct_ReturnsBardRequest_WhenModelStateIsNotValid()
        {
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };
            var newProduct = new Product { Id = 1, Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };

            var controller = new ProductsController(SettingUpUowForPostProduct(newProduct,Status.Success));
            controller.ModelState.AddModelError("", "error message");

            var actionResult = await controller.PostProduct(newProductDto);
            Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostProduct_ReturnsBardRequest_WhenCodeIsNotUnique()
        {
            var newProductDto = new ProductDto { Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };
            var newProduct = new Product { Id = 1, Code = "code1", Nom = "product1", DateDebutValidation = new DateTime(2021, 05, 06), DateFinValidation = new DateTime(2021, 05, 5) };

            var controller = new ProductsController(SettingUpUowForPostProduct(newProduct, Status.Failure));

            var actionResult = await controller.PostProduct(newProductDto);
            var badRequestRest = Assert.IsAssignableFrom<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductNotExists()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Product>(null));
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(uow => uow.ProductRepository).Returns(mockRepo.Object);

            var controller = new ProductsController(mockUow.Object);

            var actionResult = await controller.GetProduct(1);
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetProduct_ReturnsOk_WhenProductExists()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(TestData.GetTestProducts().First()));
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(uow => uow.ProductRepository).Returns(mockRepo.Object);

            var controller = new ProductsController(mockUow.Object);

            var actionResult = await controller.GetProduct(1);
            Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        }

        private IUnitOfWork SettingUpUowForPostProduct(Product productToAdd,Status resultStatus)
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(productToAdd);
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.SaveChangesAsync(new CancellationToken())).ReturnsAsync(new Result { Status = resultStatus });
            mockUow.SetupGet(uow => uow.ProductRepository).Returns(mockRepo.Object);

            return mockUow.Object;
        }

        
    }
}
