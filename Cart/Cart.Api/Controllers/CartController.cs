using Cart.Api.Entities;
using Cart.Api.Repositories;
using Cart.Api.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartReadOnly _readOnly;
        private readonly ICartWriteOnly _writeOnly;
        private readonly IHttpClientFactory _httpClient;

        public CartController(ICartReadOnly readOnly, ICartWriteOnly writeOnly, IHttpClientFactory httpClient)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
            _httpClient = httpClient;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(long userId)
        {
            var cart = await _readOnly.GetCartByUserId(userId);

            return Ok(cart);
        }

        [HttpGet("add-product")]
        public async Task<IActionResult> AddProductToCart([FromQuery]long userId, [FromQuery]string productId)
        {
            var clientR = _httpClient.CreateClient("catalog");

            using(var client = await clientR.GetAsync($"api/catalog/product-id/{productId}"))
            {
                var content = await client.Content.ReadAsStringAsync();

                if(client.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<ProductResponse>(content);
                    var product = new ShoppingProduct()
                    {
                        ProductId = productId,
                        ProductName = response.Name,
                        UnitPrice = float.TryParse(response.Price, out var price) ? price : 0f,
                        UserId = userId
                    };

                    var shoppingProduct = await _readOnly.GetCartByUserId(userId);

                    shoppingProduct.ShoppingProducts.Add(product);

                    return Ok(shoppingProduct);
                } else
                {
                    return BadRequest();
                }
            }
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
