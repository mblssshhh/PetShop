namespace PetShopWeb.Data.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public int CategoryId { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Busket> Buskets { get; set; }
    }

}
