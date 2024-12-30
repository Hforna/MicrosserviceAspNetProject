using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions dbContext) : base(dbContext)
        {
        }
    }
}
