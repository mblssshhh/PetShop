namespace PetShopWeb.Data.Entity
{
    public class Busket
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public Buyer Buyer { get; set; }
        public Product Product { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
