using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public partial class PaymentType
    {
        public int id_paymenttype { get; set; }
        public string paymenttype_name { get; set; }
    }
}
