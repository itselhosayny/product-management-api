using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagement.API.Dto;
using ProductManagement.API.Helper;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
        {
            var createdProduct = _unitOfWork.ProductRepository.Add(Mapper.MapFrom(product));
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            return null;
        }

        
    }
}
