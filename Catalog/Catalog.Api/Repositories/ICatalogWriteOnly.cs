using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface ICatalogWriteOnly
    {
        public Task AddProduct(Product product);
    }
}
