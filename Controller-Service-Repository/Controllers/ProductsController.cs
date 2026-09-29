using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Controller_Service_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductService _productService = new ProductService();

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            List<ProductForReadDto> productsDto = _productService.GetAllProducts();

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProductForCreateDto product)
        {
            var newProduct = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductForUpdateDto product)
        {
            var updated = _productService.UpdateProduct(id, product);
            if(!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var deleted = _productService.DeleteProduct(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent(); 
        }
        [HttpGet("search")]
        public ActionResult<List<ProductForReadDto>> Search([FromQuery] string name)
        {
            var results = _productService.SearchProductsByName(name ?? string.Empty);
            return Ok(results);
        }
        [HttpGet("stats")]
        public ActionResult<ProductStatsDto> GetStats()
        {
            var stats = _productService.GetStats();
            return Ok(stats);
        }
    }
}
