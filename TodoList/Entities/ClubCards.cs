namespace Domain.Entities
{
    public class ClubCard
    {
        public int Id { get; set; }

        public int IdBuyer { get; set; }

        public virtual Buyer Buyer { get; set; }

        public int Balance { get; set; }
    }
}
