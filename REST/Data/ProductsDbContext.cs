using Microsoft.EntityFrameworkCore;
using Module1.Models;

namespace Module1.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}