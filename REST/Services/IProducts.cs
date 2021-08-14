using System.Collections.Generic;
using Module1.Models;

namespace Module1.Services
{
    public interface IProducts
    {
        // CRUD operations
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}