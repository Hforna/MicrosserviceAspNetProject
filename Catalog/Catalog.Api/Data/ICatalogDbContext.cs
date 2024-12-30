using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public interface ICatalogDbContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
