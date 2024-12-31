using Cart.Api.Entities;

namespace Cart.Api.Repositories
{
    public interface ICartWriteOnly
    {
        public Task<CartEntitie> CreateCart(CartEntitie entity, long userId);
        public Task DeleteCart(long userId);
        public Task<CartEntitie> UpdateCart(List<ShoppingProduct> product, long userId);
    }
}
