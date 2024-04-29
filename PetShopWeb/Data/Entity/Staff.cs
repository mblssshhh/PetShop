namespace PetShopWeb.Data.Entity
{
    public class Staff
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Post { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
