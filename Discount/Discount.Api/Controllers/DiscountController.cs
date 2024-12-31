using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountReadOnly _readOnly;
        private readonly IDiscountWriteOnly _writeOnly;

        public DiscountController(IDiscountReadOnly readOnly, IDiscountWriteOnly writeOnly)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody]DiscountEntitie request)
        {
            var result = await _writeOnly.CreateDiscount(request);

            return Created(string.Empty, result);
        }

        [HttpGet("{productName}")]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            var result = await _readOnly.GetDiscountByProductName(productName);

            return Ok(result);
        }

        [HttpGet("all-coupons")]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var result = await _readOnly.GetDiscounts();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody]DiscountEntitie request)
        {
            var result = await _writeOnly.UpdateDiscount(request);

            return Ok(result);
        }

        [HttpDelete("{discountId}")]
        public async Task<IActionResult> DeleteDiscount(long discountId)
        {
            await _writeOnly.DeleteDiscount(discountId);

            return NoContent();
        }
    }
}
