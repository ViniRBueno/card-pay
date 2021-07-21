using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public class Recipt
    {
        public int id_recipt { get; set; }
        public int id_ticket { get; set; }
        public decimal recipt_value { get; set; }
    }
}
