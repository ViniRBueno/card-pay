using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public class Ticket
    {
        public int id_ticket { get; set; }
        public int id_debt { get; set; }
        public decimal ticket_value { get; set; }
        public DateTime ticket_data { get; set; }
        public bool blocked { get; set; }
    }
}
