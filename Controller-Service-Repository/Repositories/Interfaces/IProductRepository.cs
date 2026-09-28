
using Controller_Service_Repository.Entities;

namespace Controller_Service_Repository.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        public Product? GetProductById (int id);
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product); 
    }
}
