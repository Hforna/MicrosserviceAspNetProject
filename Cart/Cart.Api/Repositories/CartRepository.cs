using Cart.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.Api.Repositories
{
    public class CartRepository : ICartWriteOnly, ICartReadOnly
    {
        private readonly IDistributedCache _cache;

        public CartRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<CartEntitie> CreateCart(CartEntitie entity, long userId)
        {
            await _cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize<CartEntitie>(entity));

            return await GetCartByUserId(userId);
        }

        public async Task DeleteCart(long userId)
        {
            await _cache.RemoveAsync(userId.ToString());
        }

        public async Task<CartEntitie> GetCartByUserId(long userId)
        {
            var cart = await _cache.GetAsync(userId.ToString());

            return JsonSerializer.Deserialize<CartEntitie>(cart)!;
        }

        public async Task<CartEntitie> UpdateCart(List<ShoppingProduct> product, long userId)
        {
            var getUserCart = await _cache.GetStringAsync(userId.ToString());

            var cartObj = JsonSerializer.Deserialize<CartEntitie>(getUserCart);
            cartObj.ShoppingProducts.AddRange(product);

            await _cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize<CartEntitie>(cartObj));

            return await GetCartByUserId(userId);
        }
    }
}
