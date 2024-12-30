using AutoMapper;
using Catalog.Api.Entities;
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
        private readonly SqidsEncoder<long> _sqIds;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogReadOnly readOnly, ICatalogWriteOnly writeOnly, 
            SqidsEncoder<long> sqIds, IMapper mapper)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
            _sqIds = sqIds;
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
    }
}
