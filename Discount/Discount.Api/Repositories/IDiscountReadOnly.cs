using Discount.Api.Entities;

namespace Discount.Api.Repositories
{
    public interface IDiscountReadOnly
    {
        public Task<DiscountEntitie> GetDiscountByProductName(string productName);
        public Task<List<DiscountEntitie>> GetDiscounts();
    }
}
