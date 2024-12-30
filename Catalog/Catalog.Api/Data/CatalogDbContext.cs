using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogDbContext : ICatalogDbContext
    {
        private IMongoDatabase _database;

        public CatalogDbContext(IConfiguration configuration)
        {
            var mongoUrl = MongoUrl.Create(configuration.GetConnectionString("mongoConnection"));
            var client = new MongoClient(configuration.GetConnectionString("mongoConnection"));
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}
