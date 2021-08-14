using System.Collections.Generic;
using System.Linq;
using Module1.Data;
using Module1.Models;

namespace Module1.Services
{
    public class ProductRepository : IProducts
    {
        public ProductRepository(ProductsDbContext productsDbContext)
        {
            _productsDbContext = productsDbContext;
        }

        private ProductsDbContext _productsDbContext;
        
        void IProducts.AddProduct(Product product)
        {
            _productsDbContext.Products.Add(product);
            _productsDbContext.SaveChanges(true);
        }

        void IProducts.DeleteProduct(int id)
        {
            var product = _productsDbContext.Products.Find(id);

            _productsDbContext.Products.Remove(product);
            _productsDbContext.SaveChanges(true);
        }

        Product IProducts.GetProduct(int id)
        {
            return _productsDbContext.Products.SingleOrDefault (m => m.ProductId == id );
        }

        IEnumerable<Product> IProducts.GetProducts()
        {
            return _productsDbContext.Products;
        }

        void IProducts.UpdateProduct(Product product)
        {
            _productsDbContext.Products.Update(product);
            _productsDbContext.SaveChanges(true);
        }
    }
}