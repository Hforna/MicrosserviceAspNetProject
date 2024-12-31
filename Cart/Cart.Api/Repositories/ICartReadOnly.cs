using Cart.Api.Entities;

namespace Cart.Api.Repositories
{
    public interface ICartReadOnly
    {
        public Task<CartEntitie> GetCartByUserId(long  userId);
    }
}
