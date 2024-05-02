﻿namespace PetShopWeb.Data.Entity
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<ClubCard> ClubCards { get; set; }
    }

}