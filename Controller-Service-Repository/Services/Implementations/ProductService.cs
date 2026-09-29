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
        private readonly ProductRepository _repository = new ProductRepository();

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var existingProducts = _repository.GetAllProducts();
            bool nameExists = existingProducts.Any(p => p.Name == dto.Name);
            if (nameExists)
            {
                throw new InvalidOperationException("Ya existe un producto con ese nombre");
            }

            var newProduct = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            _repository.AddProduct(newProduct);

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
            var existingProduct = _repository.GetProductById(id);
            if (existingProduct == null) return false;

            _repository.DeleteProduct(existingProduct);
            return true;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
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
            var product = _repository.GetProductById(id);
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
            var existingProduct = _repository.GetProductById(id);
            if (existingProduct == null) return false;

            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;

            _repository.UpdateProduct(existingProduct);
            return true;
        }
        public List<ProductForReadDto> SearchProductsByName (string name)
        {
            var products = _repository.SearchProductsByName(name);
            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }
        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();
            if (!products.Any())
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = "N / A"
                };
            }
            return new ProductStatsDto
            {
                Total = products.Count(),
                AveragePrice = products.Average(p => p.Price),
                MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name
            };

        }
    }
}
