using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public partial class Debt
    {
        public int id_debt { get; set; }
        public int id_user { get; set; }
        public int id_status { get; set; }
        public decimal debt_value { get; set; }
    }
}
