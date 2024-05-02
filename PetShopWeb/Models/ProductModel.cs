namespace PetShopWeb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryId { get; set; }
    }
}
