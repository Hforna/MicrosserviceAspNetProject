using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface ICatalogReadOnly
    {
        public Task<Product> GetProductById(string Id);
        public Task<IEnumerable<Product>> GetProductsByCategory(string category);
    }
}
