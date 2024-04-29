namespace PetShopWeb.Data.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public Buyer Buyer { get; set; }
        public Product Product { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
