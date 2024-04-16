namespace Domain.Entities
{
    public class Buyer
    {
        public int Id { get; set; }

        public string Phone { get; set; }
        
        public string Email { get; set; }
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public int Password { get; set; }
    }
}
