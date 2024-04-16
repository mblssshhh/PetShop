namespace Domain.Entities
{
    public class Busket
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Count { get; set; }
    }
}
