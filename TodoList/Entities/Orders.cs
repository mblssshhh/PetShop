using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int BusketId { get; set; }

        public int Price { get; set; }

        public virtual Busket Busket { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public int IdStaff { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
