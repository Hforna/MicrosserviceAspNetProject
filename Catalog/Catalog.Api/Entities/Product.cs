using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")] 
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("price")]
        public float Price { get; set; }
        [BsonElement("stock_quantity")]
        public long Quantity { get; set; }
        [BsonElement("image")]
        public string ImageFile { get; set; }
        [BsonElement("category")]
        public string Category { get; set; }
    }
}
