using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public partial class Payment
    {
        public int id_payment { get; set; }
        public int id_debt { get; set; }
        public int id_paymenttype { get; set; }
        public decimal payment_value { get; set; }
        public int status { get; set; }
    }
}
