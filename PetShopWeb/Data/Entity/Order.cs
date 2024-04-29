namespace PetShopWeb.Data.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int StaffId { get; set; }
        public Basket Basket { get; set; }
        public Staff Staff { get; set; }
    }

}
