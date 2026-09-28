using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Repositories.Implementations;
using Controller_Service_Repository.Repositories.Interfaces;
using Controller_Service_Repository.Services.Interfaces;

namespace Controller_Service_Repository.Services.Implementations
{
    public class ProductService : IProductService  
    {
        private readonly ProductRepository repository = new ProductRepository();

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var newProduct = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            repository.AddProduct(newProduct);

            // Retorna el DTO de lectura correspondiente
            return new ProductForReadDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price
            }; ;
        }

        public bool DeleteProduct(int id)
        {
            var existingProduct = repository.GetProductById(id);
            if (existingProduct == null) return false;

            repository.DeleteProduct(existingProduct);
            return true;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            var products = repository.GetAllProducts();
            // Mapeo manual de Entidad -> DTO
            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null) return null;

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            }; ;
        }

        public bool UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var existingProduct = repository.GetProductById(id);
            if (existingProduct == null) return false;

            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;

            repository.UpdateProduct(existingProduct);
            return true;
        }
    }
}
