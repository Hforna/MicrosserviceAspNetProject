using Catalog.Api.Entities;
using Catalog.Api.Enums;

namespace Catalog.Api.Repositories
{
    public interface ICatalogWriteOnly
    {
        public Task AddProduct(Product product);
        public Task DeleteProduct(string id);
        public Task DeleteProductByCategory(CategorysEnum category);
    }
}
