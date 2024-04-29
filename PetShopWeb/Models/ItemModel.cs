using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShopWeb.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PathImg { get; set; } 
        public string Discripton { get; set; }

    }
}
