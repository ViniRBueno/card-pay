using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public partial class Order
    {
        public int id_order { get; set; }
        public int id_debt { get; set; }
        public int id_payment { get; set; }
        public int id_user { get; set; }
    }
}
