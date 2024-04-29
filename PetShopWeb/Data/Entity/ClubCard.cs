namespace PetShopWeb.Data.Entity
{
    public class ClubCard
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public decimal Balance { get; set; }
        public Buyer Buyer { get; set; }
    }

}
