using Dapper;
using Discount.Api.Controllers;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountReadOnly, IDiscountWriteOnly
    {
        private readonly IConfiguration _configuration;
        private readonly NpgsqlConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreConnection"));
        }

        public async Task<bool> CreateDiscount(DiscountEntitie discount)
        {
            var query = await _connection.ExecuteAsync("INSERT INTO coupons (ProductName, Description, Amount)" +
                "VALUES (@ProductName, @Description, @Amount)", new { ProductName = discount.ProductName, Description = discount.Description, Amount = discount.Amount });

            return Affected(query);
        }

        public async Task<DiscountEntitie> GetDiscountByProductName(string productName)
        {
            var coupon = await _connection.QueryFirstOrDefaultAsync<DiscountEntitie>("SELECT * FROM coupons WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon is null)
                throw new Exception("This product doesn't have coupon");

            return coupon;
        }

        public async Task<bool> UpdateDiscount(DiscountEntitie discount)
        {
            var query = await _connection.ExecuteAsync("UPDATE coupons SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
                new { Id = discount.Id, ProductName = discount.ProductName, Description = discount.Description, Amount = discount.Amount });

            return Affected(query);
        }

        public async Task DeleteDiscount(long id)
        {
            var query = await _connection.ExecuteAsync("DELETE FROM coupons WHERE Id = @Id", new { Id = id });

            if (!Affected(query))
                throw new Exception("Discount not deleted");
        }

        public async Task<List<DiscountEntitie>> GetDiscounts()
        {
            var query = await _connection.QueryAsync<DiscountEntitie>("SELECT * FROM coupons");

            return query.ToList();
        }

        private bool Affected(int query) => query != 0;
    }
}
