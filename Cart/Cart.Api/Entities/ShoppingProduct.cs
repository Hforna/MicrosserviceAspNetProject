namespace Cart.Api.Entities
{
    public class ShoppingProduct
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public long UserId { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }
}
