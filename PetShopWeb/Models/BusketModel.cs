using PetShopWeb.Data.Entity;

namespace PetShopWeb.Models
{
    public class BusketModel
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public Product Product { get; set; }
    }
}
