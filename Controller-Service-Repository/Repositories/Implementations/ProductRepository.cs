using Controller_Service_Repository.Data;
using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Repositories.Interfaces;

namespace Controller_Service_Repository.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private AMBProductosContext _context;
        public ProductRepository(AMBProductosContext context)
        {
            _context = context;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public Product? GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }
        public void AddProduct(Product product)
        {
            product.Id = _context.Products.Any() ? _context.Products.Max(p => p.Id) + 1 : 1;
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public List<Product> SearchProductsByName(string name)
        {
            return _context.Products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
