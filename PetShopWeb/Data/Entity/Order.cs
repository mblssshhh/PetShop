namespace PetShopWeb.Data.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int BusketId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int IdStaff { get; set; }
        public Busket Busket { get; set; }
        public Staff Staff { get; set; }
    }

}
