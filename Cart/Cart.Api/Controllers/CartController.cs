using Cart.Api.Entities;
using Cart.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartReadOnly _readOnly;
        private readonly ICartWriteOnly _writeOnly;

        public CartController(ICartReadOnly readOnly, ICartWriteOnly writeOnly)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(long userId)
        {
            var cart = await _readOnly.GetCartByUserId(userId);

            return Ok(cart);
        }
        
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateCart([FromBody]CartEntitie request, long userId)
        {
            var result = await _writeOnly.CreateCart(request, userId);

            return Created(string.Empty, result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCart([FromBody]List<ShoppingProduct> request, long userId)
        {
            var cart = await _writeOnly.UpdateCart(request, userId);

            return Ok(cart);
        }
        
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCart(long userId)
        {
            await _writeOnly.DeleteCart(userId);

            return NoContent();
        }
    }
}
