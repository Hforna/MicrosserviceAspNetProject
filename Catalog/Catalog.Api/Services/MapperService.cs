using AutoMapper;
using Catalog.Api.Entities;
using Catalog.Api.Requests;

namespace Catalog.Api.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<RequestCreateProduct, Product>();
        }
    }
}
