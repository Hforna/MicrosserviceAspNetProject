namespace Cart.Api.Entities
{
    public class CartEntitie
    {
        public long UserId { get; set; }
        public List<ShoppingProduct> ShoppingProducts { get; set; }
        public float TotalPrice {
            get
            {
                float totalPrice = 0;
                foreach(var product in ShoppingProducts)
                {
                    totalPrice += product.TotalPrice;
                }
                return totalPrice;
            }
        }
        public bool Active { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
