using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository : ICatalogReadOnly, ICatalogWriteOnly
    {
        private readonly ICatalogDbContext _dbContext;
        public CatalogRepository(ICatalogDbContext dbContext) => _dbContext = dbContext;

        public async Task AddProduct(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
        }

        public async Task DeleteProduct(string id)
        {
            var product = _dbContext.Products.FindAsync(x => x.Id == id);

            if (product == null)
                throw new Exception("Product not found");

            await _dbContext.Products.DeleteOneAsync(id);
        }

        public async Task DeleteProductByCategory(CategorysEnum category)
        {
            await _dbContext.Products.DeleteManyAsync(d => d.Category == category.ToString());
        }

        public async Task<Product> GetProductById(string Id)
        {
            var find = await _dbContext.Products.FindAsync(d => d.Id == Id);

            if (find is null)
                throw new Exception("Product not found");

            return await find.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var products = Builders<Product>.Filter.Eq(d => d.Category, category);
            return await _dbContext.Products.Find(products).ToListAsync();
        }
    }
}
