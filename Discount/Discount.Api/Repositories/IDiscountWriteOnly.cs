using Discount.Api.Entities;

namespace Discount.Api.Repositories
{
    public interface IDiscountWriteOnly
    {
        public Task<bool> CreateDiscount(DiscountEntitie discount);
        public Task<bool> UpdateDiscount(DiscountEntitie discount);
        public Task DeleteDiscount(long id);
    }
}
