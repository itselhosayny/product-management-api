using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Helper;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.UOW;
using ProductManagement.Domain.Validation;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProductManagement.API.Models;

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

        // GET: api/Products
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        // POST: api/Products
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdProduct = _unitOfWork.ProductRepository.Add(Mapper.MapFrom(product));
            var result = await _unitOfWork.SaveChangesAsync();
            if(result.Status == Status.Failure)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

    }
}
