using AutoMapper;
using Catalog.Api.Entities;
using Catalog.Api.Enums;
using Catalog.Api.Repositories;
using Catalog.Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqids;

namespace Catalog.Api.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogReadOnly _readOnly;
        private readonly ICatalogWriteOnly _writeOnly;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogReadOnly readOnly, ICatalogWriteOnly writeOnly, 
            IMapper mapper)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]RequestCreateProduct request)
        {
            var product = _mapper.Map<Product>(request);
            await _writeOnly.AddProduct(product);

            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> ProductById(string id)
        {
            var product = await _readOnly.GetProductById(id);

            return Ok(product);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> ProductsByCategory(string category)
        {
            var product = await _readOnly.GetProductsByCategory(category);

            return Ok(product);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            await _writeOnly.DeleteProduct(id);

            return NoContent();
        }

        [HttpDelete("delete-by-category/{category}")]
        public async Task<IActionResult> DeleteProductByCategory(CategorysEnum category)
        {
            await _writeOnly.DeleteProductByCategory(category);

            return NoContent();
        }
    }
}
