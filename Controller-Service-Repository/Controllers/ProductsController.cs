using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Services.Implementations;
using Controller_Service_Repository.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller_Service_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            List<ProductForReadDto> productsDto = _service.GetAllProducts();

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProductForCreateDto product)
        {
            try
            {
                 var newProduct = _service.CreateProduct(product);
                 return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
           
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductForUpdateDto product)
        {
            var updated = _service.UpdateProduct(id, product);
            if(!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var deleted = _service.DeleteProduct(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent(); 
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var results = _service.SearchProductsByName(name ?? string.Empty);
            return Ok(results);
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _service.GetStats();
            return Ok(stats);
        }
    }
}
