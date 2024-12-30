namespace Catalog.Api.Requests
{
    public class RequestCreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public long Quantity { get; set; }
        public string ProductImage { get; set; }
    }
}
